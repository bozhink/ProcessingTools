namespace ProcessingTools.Mediatypes.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts;
    using ProcessingTools.Mediatypes.Data.Entity.Models;
    using ProcessingTools.Mediatypes.Data.Seed.Contracts;
    using ProcessingTools.Mediatypes.Data.Seed.Models;

    public class MediatypesDataSeeder : IMediatypesDataSeeder
    {
        private readonly IMediatypesDbContextProvider contextProvider;

        private ConcurrentQueue<Exception> exceptions;

        public MediatypesDataSeeder(IMediatypesDbContextProvider contextProvider)
        {
            this.contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            ExtensionJson[] mediatypesJson = this.ParseDataJsonFile();

            if (mediatypesJson.Length < 1)
            {
                throw new ProcessingTools.Exceptions.InvalidDataException("Mediatypes data json file is empty or invalid.");
            }

            await this.ImportMimeTypesToDatabaseAsync(mediatypesJson);
            await this.ImportMimeSubtypesToDataBaseAsync(mediatypesJson);
            await this.ImportFileExtensionsToDatabaseAsync(mediatypesJson);
            await this.CreateMediaTypePairsInDatabaseAsync(mediatypesJson);
            await this.ConnectMediaTypePairsToFileExtensionsAsync(mediatypesJson);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private ExtensionJson[] ParseDataJsonFile()
        {
            string jsonFilePath = AppSettings.MediaTypeDataJsonFileName;

            string jsonString = File.ReadAllText(jsonFilePath);

            var mediatypesJson = JsonConvert.DeserializeObject<ExtensionJson[]>(jsonString);
            return mediatypesJson?.Select(m => new ExtensionJson
            {
                Extension = m.Extension.ToLowerInvariant(),
                Mimetype = m.Mimetype.ToLowerInvariant(),
                Mimesubtype = m.Mimesubtype.ToLowerInvariant(),
                Description = m.Description
            })
            .ToArray();
        }

        private async Task ConnectMediaTypePairsToFileExtensionsAsync(ExtensionJson[] mediatypesJson)
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task CreateMediaTypePairsInDatabaseAsync(ExtensionJson[] mediatypesJson)
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportFileExtensionsToDatabaseAsync(ExtensionJson[] mediatypesJson)
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportMimeSubtypesToDataBaseAsync(ExtensionJson[] mediatypesJson)
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportMimeTypesToDatabaseAsync(ExtensionJson[] mediatypesJson)
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
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}
