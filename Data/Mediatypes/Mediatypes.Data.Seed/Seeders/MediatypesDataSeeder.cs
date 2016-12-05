using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProcessingTools.Mediatypes.Data.Entity.Contracts;
using ProcessingTools.Mediatypes.Data.Entity.Models;
using ProcessingTools.Mediatypes.Data.Seed.Contracts;
using ProcessingTools.Mediatypes.Data.Seed.Models;

namespace ProcessingTools.Mediatypes.Data.Seed.Seeders
{
    public class MediatypesDataSeeder : IMediatypesDataSeeder
    {
        private const string MediaTypeDataJsonFilePathKey = "MediaTypeDataJsonFilePath";

        private readonly IMediatypesDbContextProvider contextProvider;

        private ConcurrentQueue<Exception> exceptions;

        public MediatypesDataSeeder(IMediatypesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            ExtensionJson[] mediatypesJson = this.ParseDataJsonFile();

            if (mediatypesJson.Length < 1)
            {
                throw new ApplicationException("Mediatypes data json file is empty or invalid.");
            }

            await this.ImportMimeTypesToDatabase(mediatypesJson);
            await this.ImportMimeSubtypesToDataBase(mediatypesJson);
            await this.ImportFileExtensionsToDatabase(mediatypesJson);
            await this.CreateMediaTypePairsInDatabase(mediatypesJson);
            await this.ConnectMediaTypePairsToFileExtensions(mediatypesJson);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private ExtensionJson[] ParseDataJsonFile()
        {
            string jsonFilePath = ConfigurationManager.AppSettings[MediaTypeDataJsonFilePathKey];

            string jsonString = File.ReadAllText(jsonFilePath);

            var mediatypesJson = JsonConvert.DeserializeObject<ExtensionJson[]>(jsonString);
            return mediatypesJson?.Select(m => new ExtensionJson
            {
                Extension = m.Extension.ToLower(),
                Mimetype = m.Mimetype.ToLower(),
                Mimesubtype = m.Mimesubtype.ToLower(),
                Description = m.Description
            })
            .ToArray();
        }

        private async Task ConnectMediaTypePairsToFileExtensions(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    context.Configuration.UseDatabaseNullSemantics = true;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    var mimeTypePairs = context.MimetypePairs.ToList();
                    var extensions = context.FileExtensions.ToList();

                    foreach (var extension in extensions)
                    {
                        var extensionData = new HashSet<ExtensionJson>(mediatypesJson
                            .Where(m => m.Extension == extension.Name));

                        var pairs = new HashSet<MimetypePair>(extensionData.Select(d =>
                            mimeTypePairs.FirstOrDefault(p =>
                                (p.Mimetype.Name == d.Mimetype) && (p.Mimesubtype.Name == d.Mimesubtype))));

                        foreach (var pair in pairs)
                        {
                            extension.MimetypePairs.Add(pair);
                        }
                    }

                    context.FileExtensions.AddOrUpdate(extensions.ToArray());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task CreateMediaTypePairsInDatabase(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    var mimeTypes = context.Mimetypes.ToList();
                    var mimeSubtypes = context.Mimesubtypes.ToList();

                    var mediaTypesPairs = mediatypesJson
                        .Select(p => new MimetypePair
                        {
                            Mimetype = mimeTypes.FirstOrDefault(m => m.Name == p.Mimetype),
                            Mimesubtype = mimeSubtypes.FirstOrDefault(s => s.Name == p.Mimesubtype)
                        })
                        .ToArray();

                    context.MimetypePairs.AddOrUpdate(mediaTypesPairs);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportFileExtensionsToDatabase(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    var fileExtension = new HashSet<string>(mediatypesJson.Select(e => e.Extension))
                    .Select(e => new FileExtension
                    {
                        Name = e
                    })
                    .ToArray();

                    context.FileExtensions.AddOrUpdate(fileExtension);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportMimeSubtypesToDataBase(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    var mimeSubtypesNames = new HashSet<string>(mediatypesJson.Select(t => t.Mimesubtype))
                    .Select(s => new Mimesubtype
                    {
                        Name = s
                    })
                    .ToArray();

                    context.Mimesubtypes.AddOrUpdate(mimeSubtypesNames);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportMimeTypesToDatabase(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    var mimeTypesNames = new HashSet<string>(mediatypesJson.Select(t => t.Mimetype))
                    .Select(m => new Mimetype
                    {
                        Name = m
                    })
                    .ToArray();

                    context.Mimetypes.AddOrUpdate(mimeTypesNames);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
