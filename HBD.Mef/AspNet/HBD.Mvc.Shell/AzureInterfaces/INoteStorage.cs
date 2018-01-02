#region using

using AzureNoteEntities;

#endregion

namespace AzureInterfaces
{
    public interface INoteStorage : IDocumentStorage<Note, string>
    {
    }
}