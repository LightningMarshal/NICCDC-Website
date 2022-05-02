using Microsoft.AspNetCore.Mvc;

namespace ProjectSaturn.Controllers
{
    public class NICCDCAnalyticsController : Controller
    {
        /* This is a purely theoretical controller. This controller would be used to set up a second site that would interact with
         * the database and show applications for each user. This controller would also allow for those authorized to change or remove
         * data from the database. 
         * 
         * If this is the route that would like to be taken, please add the following table columns to the database tables:
         * DeletedByID uniqueidentifier
         * DeletedDate datetime2
         * IsDeleted bit
         * 
         * These will allow accountablility for data deletion and will only soft delete data, allowing for possible recovery if needed.
         * 
         * I hope that this will help you in creating this feature if this is the route that is taken.
         * 
         * NOTE: Some of the data is Json Serialized in order to keep a varying size list in the same place. Please use to deserialize:
         * -DeserializeObject<List<*data type (string)*>>
        */
    }
}
