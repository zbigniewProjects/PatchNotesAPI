namespace DestructorsNetApi.Contracts.PatchNotes
{
    public record PostPatchNoteReq(
        string Version, 
        string Title, 
        string Notes
        );
}
