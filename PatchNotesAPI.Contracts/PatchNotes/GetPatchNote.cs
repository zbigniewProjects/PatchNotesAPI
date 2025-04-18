

namespace DestructorsNetApi.Contracts.PatchNotes
{
    public record GetPatchNote(
        string Version,
        string Title,
        string Notes,
        string Date
        );

    public record GetPatches(
        int ElementsLeft,
        GetPatchNote[] Elements
        );
}
