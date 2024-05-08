using BackendForFrontend;
using BusinessLogicLayer;
using Grpc.Core;

namespace BackendForFrontendService.Services
{
  public class DocumentService : Documents.DocumentsBase
  {
    private readonly ILogger<DocumentService> _logger;
    private readonly DocumentManager _documentManager;

    public DocumentService(ILogger<DocumentService> logger, DocumentManager documentManager)
    {
      _logger = logger;
      _documentManager = documentManager;
    }

    public override async Task<DocumentsReply> GetDocuments(DocumentsRequest request, ServerCallContext context)
    {
      var documents = await _documentManager.GetDocumentsAsync();
      var reply = new DocumentsReply();
      foreach (var document in documents)
      {
        reply.Documents.Add(new DocumentReply
        {
          Id = document.Id,
          Name = document.Name
        });
      }
      return reply;
    }
  }
}
