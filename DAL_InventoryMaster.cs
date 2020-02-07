using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunshiOrganicsDAL
{
   public static class DAL_InventoryMaster
    {
        #region Constants..
        const string EnteringProcedure = ">>>>";
        const string ExitingProcedure = "<<<<";
        const string ErrorIn = "Error in - ";
        const string Dash = " - ";
        #endregion
        #region Insert_Upadte_Delete
 
        public static int InventoryMasterInsert(string strConn, int InventoryId, string PersonName, string Place, int Quantity, string Unit, int Type, Guid? CompanyId)
        {
            const string ProcName = "Inventory_InsertUpdate()";
            SqlConnection connection = null;
            SqlParameter param;
            DateTime startDate = DateTime.Now;
           Guid LoginID = Guid.Empty;
            string strMessage = string.Empty;
            DataTable dt = new DataTable();
            int rowsEffected;
            int returnValue;
            try
            {
                connection = DataSource.GetConnection(strConn);
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = ProcName.Substring(0, ProcName.Length - 2);
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    param = command.Parameters.Add("@InventoryId", SqlDbType.Int);
                    param.Value =InventoryId;


                    param = command.Parameters.Add("@PersonName", SqlDbType.VarChar);
                    param.Value = PersonName;


                    param = command.Parameters.Add("@Place", SqlDbType.VarChar);
                    param.Value = Place;

                    param = command.Parameters.Add("@Quantity", SqlDbType.Int);
                    param.Value = Quantity;

                    param = command.Parameters.Add("@Unit", SqlDbType.VarChar);
                    param.Value = Unit;
                    param = command.Parameters.Add("@Type", SqlDbType.Int);
                    param.Value = Type;
                    param = command.Parameters.Add("@CompanyId", SqlDbType.Int);
                    param.Value = CompanyId;

                    param = command.Parameters.Add("@ReturnValue", SqlDbType.Int);
                    param.Direction = ParameterDirection.ReturnValue;

                    rowsEffected = command.ExecuteNonQuery();
                    returnValue = (int)command.Parameters["@ReturnValue"].Value;



                }
            }
            finally
            {
                DataSource.CloseConnection(connection);

                TimeSpan timeSpan = DateTime.Now.Subtract(startDate);
            }
            return returnValue;

        }

        #endregion
        #region Process_List
        public static DataSet InventoryMaster_List(string strConn, 
            int SelectionType, string SearchBy, string SearchString, int InventoryId, Guid CompanyId,
               int ItemsPerPage, int RequestPageNo, int CurrentPageNo)
        {
            const string ProcName = "Inventory_Select";
            SqlConnection connection = new SqlConnection();
            SqlParameter param;
            DataSet outDS = null;
            try
            {
                connection = DataSource.GetConnection(strConn);
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = ProcName;
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;


                    param = command.Parameters.Add("@SelectionType", SqlDbType.Int);
                    param.Value = SelectionType;
                    param = command.Parameters.Add("@SearchBy", SqlDbType.VarChar);
                    if (SearchBy != null)
                        param.Value = SearchBy;
                    else
                        param.Value = DBNull.Value;
                    param = command.Parameters.Add("@SearchString", SqlDbType.VarChar);
                    if (SearchBy != null)
                        param.Value = SearchString;
                    else
                        param.Value = "";
                    param = command.Parameters.Add("@InventoryId", SqlDbType.Int);
                    param.Value = InventoryId;


                    param = command.Parameters.Add("@CompanyId", SqlDbType.UniqueIdentifier);
                    param.Value = CompanyId;

                    param = command.Parameters.Add("@ItemsPerPage", SqlDbType.Int);
                    param.Value = ItemsPerPage;

                    param = command.Parameters.Add("@RequestPageNo", SqlDbType.Int);
                    param.Value = RequestPageNo;

                    param = command.Parameters.Add("@CurrentPageNo", SqlDbType.Int);
                    param.Value = CurrentPageNo;

                    param = command.Parameters.Add("@ReturnValue", SqlDbType.Int);
                    param.Direction = ParameterDirection.ReturnValue;

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        outDS = new DataSet();
                        da.Fill(outDS);
                    }
                }
            }
            finally
            {
                DataSource.CloseConnection(connection);


            }
            return outDS;
        }

        
    }
}
#endregion
    

