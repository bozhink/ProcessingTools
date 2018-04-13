// <copyright file="ApplicationRolesController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Web.Documents.Data;
    using ProcessingTools.Web.Documents.Models;

    /// <summary>
    /// Application roles controller.
    /// </summary>
    public class ApplicationRolesController : Controller
    {
        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Details action name.
        /// </summary>
        public const string DetailsActionName = nameof(Details);

        /// <summary>
        /// Create action name.
        /// </summary>
        public const string CreateActionName = nameof(Create);

        /// <summary>
        /// Edit action name.
        /// </summary>
        public const string EditActionName = nameof(Edit);

        /// <summary>
        /// Delete action name.
        /// </summary>
        public const string DeleteActionName = nameof(Delete);

        private readonly ApplicationDbContext context;

        public ApplicationRolesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: ApplicationRoles
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.ApplicationRoles.ToListAsync());
        }

        // GET: ApplicationRoles/Details/5
        [ActionName(DetailsActionName)]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var applicationRole = await this.context.ApplicationRoles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return this.NotFound();
            }

            return this.View(applicationRole);
        }

        // GET: ApplicationRoles/Create
        [ActionName(CreateActionName)]
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: ApplicationRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(applicationRole);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(IndexActionName);
            }

            return this.View(applicationRole);
        }

        // GET: ApplicationRoles/Edit/5
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var applicationRole = await this.context.ApplicationRoles.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return this.NotFound();
            }

            return this.View(applicationRole);
        }

        // POST: ApplicationRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (id != applicationRole.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(applicationRole);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ApplicationRoleExists(applicationRole.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(IndexActionName);
            }

            return this.View(applicationRole);
        }

        // GET: ApplicationRoles/Delete/5
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var applicationRole = await this.context.ApplicationRoles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return this.NotFound();
            }

            return this.View(applicationRole);
        }

        // POST: ApplicationRoles/Delete/5
        [HttpPost]
        [ActionName(DeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationRole = await this.context.ApplicationRoles.SingleOrDefaultAsync(m => m.Id == id);
            this.context.ApplicationRoles.Remove(applicationRole);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(IndexActionName);
        }

        private bool ApplicationRoleExists(string id)
        {
            return this.context.ApplicationRoles.Any(e => e.Id == id);
        }
    }
}
