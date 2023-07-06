using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

public partial class artistDetails : System.Web.UI.Page
{
    public int PageNumber = 1;
    public int itemsPerPage = 10;
    public int count = 0;
    public List<ArtistDetailsModel> ArtistDetailsModels = new List<ArtistDetailsModel>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
    }

    public ICollection<ArtistDetailsModel> GetArtistDetails()
    {
        var pageDetails = new Paging();
        ICollection<ArtistDetailsModel> result;
        if (ArtistDetailsModels?.Any() ?? false)
        {
            result = pageDetails.GetPaginated(PageNumber + count, itemsPerPage, ArtistDetailsModels);
            count++;
            return result;
        }

        var sql = new SQL(60);
        SqlDataReader resultReader = sql.ExecuteStoredProcedureDataReader("GetArtistDetails", true);
        //List<ArtistDetailsModel> artistGroup = new List<ArtistDetailsModel>();

        while (resultReader.Read())
        {
            ArtistDetailsModels.Add(new ArtistDetailsModel
            {
                ArtistID = (int)resultReader["ArtistID"],
                ArtistName = (string)resultReader["ArtistName"] ?? string.Empty,
                Biography = (string)resultReader["ArtistBiography"] ?? string.Empty,
                ArtistImageURL = (string)resultReader["ArtistImageURL"] ?? string.Empty,
                AlbumID = (int)resultReader["AlbumID"],
                AlbumTitle = (string)resultReader["AlbumTitle"] ?? string.Empty,
                AlbumImageURL = (string)resultReader["AlbumImageURL"] ?? string.Empty,
                SongID = (int)resultReader["SongID"],
                SongTitle = (string)resultReader["SongTitle"] ?? string.Empty,
                SongBpm = (decimal)resultReader["BPM"],
                SongTimeSignature = (string)resultReader["TimeSignature"] ?? string.Empty,
            });
        }
        sql.CloseConnection();
        
        result = pageDetails.GetPaginated(PageNumber, itemsPerPage, ArtistDetailsModels);
        return result;
    }
}

public class ArtistDetailsModel
{
    public int ArtistID { get; set; }

    public string ArtistName { get; set; }

    public string Biography { get; set; }

    public string ArtistImageURL { get; set; }

    public int AlbumID { get; set; }

    public string AlbumTitle { get; set; }

    public string AlbumImageURL { get; set; }

    public int SongID { get; set; }

    public string SongTitle { get; set; }

    public decimal SongBpm { get; set; }

    public string SongTimeSignature { get; set; }
}