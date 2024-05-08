using BackendForFrontend;
using Microsoft.AspNetCore.Components;
using static BackendForFrontend.Documents;

namespace WebFrontend.Components.Pages
{
  public partial class DocumentsPage
  {
    [Inject]
    private DocumentsClient DocumentsClient { get; set; }

    private List<DocumentReply> Documents { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
      var reply = await DocumentsClient.GetDocumentsAsync(new DocumentsRequest());
      Documents = reply.Documents.ToList();
    }
    
  }
}
