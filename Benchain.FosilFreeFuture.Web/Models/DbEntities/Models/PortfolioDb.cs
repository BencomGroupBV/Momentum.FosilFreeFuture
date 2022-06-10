namespace Benchain.FosilFreeFuture.Web.Models.DbEntities;

public class PortfolioDb
{
  public int Id { get; set; }

  public string Name { get; set; }
    
  public int Balance { get; set; }
   
  public int ParticipantId { get; set; }
    
  public int ProfileId { get; set; }
  
  public int Status { get; set; }

  public int CheckIt { get; set; }

}