using DestructorsNetApi.Contracts.PatchNotes;
using DestructorsNetApi.Data;
using DestructorsNetApi.Middleware;
using Microsoft.EntityFrameworkCore;

namespace DestructorsNetApi.Endpoints
{
    public static class PatchNotesEndpoint
    {
        public static void MapPatchNotesEndpoints(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("api/patchnotes");
            group.MapGet("{pageID}", GetPatchNotes);
            group.MapPost("", PostPatchNote).RequireApiKey();
        }

        public static async Task<IResult> GetPatchNotes(int pageID, PatchNotesDbContext patchNotesDbContext)
        {
            if (pageID < 0)
                pageID = 0;

            int count = 3;
            int startID = pageID * count;

            PatchNote[] dbPatchNotes = await patchNotesDbContext.PatchNotes.OrderByDescending(p => p.Id)!.Skip(startID).Take(count).ToArrayAsync();
            int elementsLeft = await patchNotesDbContext.PatchNotes.OrderByDescending(p => p.Id)!.Skip(startID + dbPatchNotes.Length).CountAsync();

            //map db data to response
            GetPatchNote[] resPatches = new GetPatchNote[dbPatchNotes.Length];
            for (int i = 0; i < dbPatchNotes.Length; i++) 
            {
                PatchNote dbPatch = dbPatchNotes[i];
                resPatches[i] = new GetPatchNote(dbPatch.Version, dbPatch.Title, dbPatch.Description, dbPatch.PostedAt.ToShortDateString());
            }

            return Results.Ok(new GetPatches(elementsLeft, resPatches));
        }

        public static async Task<IResult> PostPatchNote(PostPatchNoteReq req, PatchNotesDbContext patchNotesDbContext) 
        {
            PatchNote patchNote = new()
            {
                Version = req.Version,
                Title = req.Title,
                Description = req.Notes,
                PostedAt = DateTime.UtcNow
            };
            await patchNotesDbContext.PatchNotes.AddAsync(patchNote);
            await patchNotesDbContext.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
