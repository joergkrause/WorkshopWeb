using BackendApi.DataTransferObjects;
using DataTransferObjects;
using DomainModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer;

public class DocumentManager(IServiceProvider serviceProvider) : Manager(serviceProvider)
{
  
  // TODO: Nullable
  public async Task<DocumentDto?> GetDocumentById(int id)
  {
    var model = await Context.Set<Document>().FindAsync(id);
    return Mapper.Map<DocumentDto>(model);
  }

  public async Task<IEnumerable<DocumentListDto>> GetDocumentsAsync()
  {
    var models = await Context.Set<Document>().ToListAsync();
    return Mapper.Map<IEnumerable<DocumentListDto>>(models);
  }

  public async Task<IEnumerable<DocumentListDto>> GetDocumentsByNameAsync(string name, bool sort)
  {
    var query = Context.Set<Document>()
      .Where(e => e.Name == name);
      
    if (sort)
    {
      query = query.OrderBy(e => e.Name);
    } else
    {
      query = query.OrderByDescending(e => e.Name); 
    }
    var models = await query.AsNoTracking().ToListAsync();
    return Mapper.Map<IEnumerable<DocumentListDto>>(models);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="deviceDto"></param>
  /// <exception cref="ArgumentNullException" />
  /// <returns></returns>
  public async Task<DataResult<DocumentDto>> AddDocumentAsync(CreateDocumentDto deviceDto)
  {
    var canWrite = UserContext.Principal.HasClaim("scope", "write:documents");
    if (!canWrite)
    {
      return new DataResult<DocumentDto>(null!, false, new UnauthorizedAccessException());
    }

    var model = Mapper.Map<Document>(deviceDto);
    var category = await Context.Set<Category>().FindAsync(deviceDto.CategoryId);

    ArgumentNullException.ThrowIfNull(category, nameof(category));

    model.Category = category;
    Context.Entry(model).State = EntityState.Added;
    try
    {
      await Context.SaveChangesAsync();
      var result = Mapper.Map<DocumentDto>(model);
      return new DataResult<DocumentDto>(result, true, null);
    }
    catch (DbUpdateConcurrencyException ex) // when (ex.Data.)
    {
      return new DataResult<DocumentDto>(null!, false, ex);
    }
    catch (DbUpdateException ex)
    {
      return new DataResult<DocumentDto>(null!, false, ex);
    }
  }

  public async Task<DataResult<DocumentDto>> UpdateDocumentAsync(UpdateDocumentDto deviceDto)
  {
    var model = Mapper.Map<Document>(deviceDto);
    Context.Entry(model).State = EntityState.Modified;
    try
    {
      await Context.SaveChangesAsync();
      var result = Mapper.Map<DocumentDto>(model);
      return new DataResult<DocumentDto>(result, true, null);
    }
    catch (DbUpdateConcurrencyException ex) // when (ex.Data.)
    {
      return new DataResult<DocumentDto>(null!, false, ex);
    }
    catch (DbUpdateException ex)
    {
      return new DataResult<DocumentDto>(null!, false, ex);
    }
  }

  public async Task DeleteDeviceAsync(DocumentDto device)
  {
    Context.Set<Document>().Remove(new Document { Id = device.Id });
    await Context.SaveChangesAsync();
  }

  // jahr: 1945, titel: Test,  

  public async Task<IEnumerable<int>> QueryDocuments(QueryObject query)
  {
    var name = query.Query;
    return Enumerable.Empty<int>();
  }
}
