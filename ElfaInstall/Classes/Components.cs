using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ElfaInstall.Classes
{

    public class SecureData
    {
        public string HashValue(string Data)
        {
            byte[] databyte = Encoding.ASCII.GetBytes(Data);
            MD5 hash = new MD5CryptoServiceProvider();
            byte[] result = hash.ComputeHash(databyte);
            return Convert.ToBase64String(result);
        }
        public string GenerateHash(string OrderNumb, string OrderDate, string CurrentDate)
        {
            const string salt = "{lunchbox}";
            string value = OrderNumb + ":" + OrderDate + ":" + CurrentDate;
            byte[] data = Encoding.UTF8.GetBytes(value + salt);
            data = MD5.Create().ComputeHash(data);
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2").ToLower());
            }
            return stringBuilder.ToString();
        }
        public string Encrypt(string S)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(S);
            BitArray bits = new BitArray(bytes);
            bits.Not().CopyTo(bytes, 0);
            return Convert.ToBase64String(bytes);
        }
        public string Decrypt(string S)
        {
            byte[] bytes = Convert.FromBase64String(S);
            BitArray bits = new BitArray(bytes);
            bits.Not().CopyTo(bytes, 0);
            return Encoding.ASCII.GetString(bytes);
        }
    }

    public class SessionData
    {
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConnString);

        public int CheckLogin(string Login, string Password)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_CheckLogin";
            objComm.Parameters.AddWithValue("@Login", Login);
            objComm.Parameters.AddWithValue("@Password", Password);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public SqlDataReader StoreInfo(int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetStoreInfo";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public string UserStatus(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetUserStatus";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            _objConn.Open();
            string result = objComm.ExecuteScalar().ToString();
            _objConn.Close();
            return result;
        }
        public void UpdateUser(int UserID, string UserLogin, bool Active, string UserPhone, string UserEmail)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateUser";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            objComm.Parameters.AddWithValue("@UserLogin", UserLogin);
            objComm.Parameters.AddWithValue("@Active", Active);
            objComm.Parameters.AddWithValue("@UserPhone", UserPhone);
            objComm.Parameters.AddWithValue("@UserEmail", UserEmail);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public int GetStoreID(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetStoreID";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public int GetVendorID(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetVendorID";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public int GetRegionID(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetRegionID";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public int GetOrganizerID(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetOrganizerID";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public void DeleteUser(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_DeleteUser";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public int AddNewUser(string UserName, string Login, string Password, string Status, int StoreID, int VendorID, int RegionID, string UserEmail, string UserPhone, int OrganizerID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddUserInfo";
            objComm.Parameters.AddWithValue("@UserName", UserName);
            objComm.Parameters.AddWithValue("@userLogin", Login);
            objComm.Parameters.AddWithValue("@userPassword", Password);
            objComm.Parameters.AddWithValue("@status", Status);
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@RegionID", RegionID);
            objComm.Parameters.AddWithValue("@userEmail", UserEmail);
            objComm.Parameters.AddWithValue("@userPhone", UserPhone);
            objComm.Parameters.AddWithValue("@OrganizerID", OrganizerID);
            SqlParameter parmResult = objComm.Parameters.Add("@UserID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            int userID = int.Parse(objComm.Parameters["@UserID"].Value.ToString());
            _objConn.Close();
            return userID;
        }
        public SqlDataReader UserInfo(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetUserInfo";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public string UserName(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT userLogin FROM tblUsers WHERE UserID = " + UserID;
            _objConn.Open();
            string result = objComm.ExecuteScalar().ToString();
            _objConn.Close();
            return result;
        }
        public int AddNewRegion(string RegionName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddNewRegion";
            objComm.Parameters.AddWithValue("@RegionName", RegionName);
            SqlParameter parmResult = objComm.Parameters.Add("@RegionID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            int regionID = int.Parse(objComm.Parameters["@RegionID"].Value.ToString());
            _objConn.Close();
            return regionID;
        }
        public SqlDataReader RegionList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT R.RegionID,R.RegionName FROM tblRegions R INNER JOIN tblUsers U ON R.RegionID = U.RegionID WHERE U.active=1";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader RegionInfo(int RegionID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "Select RegionID,RegionName,City,State,email,Comments,Regional FROM tblRegions WHERE RegionID=" + RegionID;
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader RegionByStoreCode(string StoreCode)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetRegionByStore";
            objComm.Parameters.AddWithValue("@StoreCode", StoreCode);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int StoreIDByCode(string StoreCode)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT StoreID FROM tblStores WHERE StoreCode='" + StoreCode +"'";
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }

        public SqlDataReader VendorsByRegion(int RegionID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetVendorsByRegion";
            objComm.Parameters.AddWithValue("@RegionID", RegionID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader StoresByRegion(int RegionID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pStoreByRegion";
            objComm.Parameters.AddWithValue("@RegionID", RegionID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void UpdatePassword(int UserID, string Password)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdatePassword";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            objComm.Parameters.AddWithValue("@Password", Password);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void SetNewPassword(int UserID, string Password)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdatePassword";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            objComm.Parameters.AddWithValue("@Password", Password);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void AddMarket(string MarketName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddMarket";
            objComm.Parameters.AddWithValue("@MarketName", MarketName);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void EditStore(int StoreID, string StoreNumb, string StoreCode, string StoreName,
                              int MarketID, string Address, string City, string State, string Phone,
                              string GManager, string GMEmail, string IC, string ICEmail, string MngTraining,
                              string MTemail, string AreaDirector, string ADemail, decimal InstProc, float InstMin, float Delivery)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateStoreInfo";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            objComm.Parameters.AddWithValue("@StoreNumb", StoreNumb);
            objComm.Parameters.AddWithValue("@StoreCode", StoreCode);
            objComm.Parameters.AddWithValue("@StoreName", StoreName);
            objComm.Parameters.AddWithValue("@MarketID", MarketID);
            objComm.Parameters.AddWithValue("@Address", Address);
            objComm.Parameters.AddWithValue("@City", City);
            objComm.Parameters.AddWithValue("@State", State);
            objComm.Parameters.AddWithValue("@phone", Phone);
            objComm.Parameters.AddWithValue("@GManager", GManager);
            objComm.Parameters.AddWithValue("@GMEmail", GMEmail);
            objComm.Parameters.AddWithValue("@IC", IC);
            objComm.Parameters.AddWithValue("@ICemail", ICEmail);
            objComm.Parameters.AddWithValue("@MngTraining", MngTraining);
            objComm.Parameters.AddWithValue("@MTemail", MTemail);
            objComm.Parameters.AddWithValue("@AreaDirector", AreaDirector);
            objComm.Parameters.AddWithValue("@ADemail", ADemail);
            objComm.Parameters.AddWithValue("@Inst_proc", InstProc);
            objComm.Parameters.AddWithValue("@Inst_min", InstMin);
            objComm.Parameters.AddWithValue("@Delivery", Delivery);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public int NewStore(string StoreNumb, string StoreCode, string StoreName,
                            int MarketID, string Address, string City, string State, string Phone,
                            string GManager, string GMEmail, string IC, string ICEmail, string MngTraining,
                            string MTemail, string AreaDirector, string ADemail, decimal InstProc, float InstMin, float Delivery)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddNewStore";
            objComm.Parameters.AddWithValue("@StoreNumb", StoreNumb);
            objComm.Parameters.AddWithValue("@StoreCode", StoreCode);
            objComm.Parameters.AddWithValue("@StoreName", StoreName);
            objComm.Parameters.AddWithValue("@MarketID", MarketID);
            objComm.Parameters.AddWithValue("@Address", Address);
            objComm.Parameters.AddWithValue("@City", City);
            objComm.Parameters.AddWithValue("@State", State);
            objComm.Parameters.AddWithValue("@phone", Phone);
            objComm.Parameters.AddWithValue("@GManager", GManager);
            objComm.Parameters.AddWithValue("@GMEmail", GMEmail);
            objComm.Parameters.AddWithValue("@IC", IC);
            objComm.Parameters.AddWithValue("@ICemail", ICEmail);
            objComm.Parameters.AddWithValue("@MngTraining", MngTraining);
            objComm.Parameters.AddWithValue("@MTemail", MTemail);
            objComm.Parameters.AddWithValue("@AreaDirector", AreaDirector);
            objComm.Parameters.AddWithValue("@ADemail", ADemail);
            objComm.Parameters.AddWithValue("@Inst_proc", InstProc);
            objComm.Parameters.AddWithValue("@Inst_min", InstMin);
            objComm.Parameters.AddWithValue("@Delivery", Delivery);
            SqlParameter parmResult = objComm.Parameters.Add("@StoreID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            int storeID = int.Parse(objComm.Parameters["@StoreID"].Value.ToString());
            _objConn.Close();
            return storeID;
        }
        public int DeleteStore(int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_DeleteStore";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            int rowsAffected = objComm.ExecuteNonQuery();
            _objConn.Close();
            return rowsAffected;
        }
        public SqlDataReader CheckNewStore(string StoreNumb, string StoreCode, string StoreName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_CheckStore";
            objComm.Parameters.AddWithValue("@StoreNumb", StoreNumb);
            objComm.Parameters.AddWithValue("@StoreCode", StoreCode);
            objComm.Parameters.AddWithValue("@StoreName", StoreName);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader VendorInfo(int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetVendorInfo";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader VendorPhones(int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetVendorPhones";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void UpdateVendor(int VendorID, string VendorName, string City, string State,
                                 string Location, string Email, string Email2, string Comments, bool Active, decimal Procent, 
            string ContracorNumb, string Address, string Zip, int PayID,bool Deduct)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateVendorInfo";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@VendorName", VendorName);
            objComm.Parameters.AddWithValue("@City", City);
            objComm.Parameters.AddWithValue("@State", State);
            objComm.Parameters.AddWithValue("@Location", Location);
            objComm.Parameters.AddWithValue("@Email", Email);
            objComm.Parameters.AddWithValue("@Email2", Email2);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Active", Active);
            objComm.Parameters.AddWithValue("@Procent", Procent);
            objComm.Parameters.AddWithValue("@ContracorNumb", ContracorNumb);
            objComm.Parameters.AddWithValue("@Address", Address);
            objComm.Parameters.AddWithValue("@Zip", Zip);
            objComm.Parameters.AddWithValue("@PayID", PayID);
            objComm.Parameters.AddWithValue("@Deduct", Deduct);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public int NewVendor(string VendorName, string City, string State,
                string Location, string Email, string Email2,string Comments, 
                decimal Procent, string ContractorNumb, DateTime? DateAdded, 
                string Address, string Zip, int PayID,bool Deduct)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddNewVendor";
            objComm.Parameters.AddWithValue("@VendorName", VendorName);
            objComm.Parameters.AddWithValue("@City", City);
            objComm.Parameters.AddWithValue("@State", State);
            objComm.Parameters.AddWithValue("@Location", Location);
            objComm.Parameters.AddWithValue("@Email", Email);
            objComm.Parameters.AddWithValue("@Email2", Email2);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Procent", Procent);
            objComm.Parameters.AddWithValue("@ContractorNumb", ContractorNumb);
            objComm.Parameters.AddWithValue("@DateAdded", DateAdded);
            objComm.Parameters.AddWithValue("@Address", Address);
            objComm.Parameters.AddWithValue("@Zip", Zip);
            objComm.Parameters.AddWithValue("@PayID", PayID);
            objComm.Parameters.AddWithValue("@Deduct", Deduct);
            SqlParameter parmResult = objComm.Parameters.Add("@VendorID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            int vendorID = int.Parse(objComm.Parameters["@VendorID"].Value.ToString());
            _objConn.Close();
            return vendorID;
        }
        public void UpdateVendorPhone(int VendorID, string Phone, string Type)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateVendorPhone";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@phone", Phone);
            objComm.Parameters.AddWithValue("@type", Type);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public int AddVendorPhone(int VendorID, string Phone, string Type)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddVendorPhone ";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@phone", Phone);
            objComm.Parameters.AddWithValue("@type", Type);
            SqlParameter parmResult = objComm.Parameters.Add("@PhoneID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            int phoneID = int.Parse(objComm.Parameters["@PhoneID"].Value.ToString());
            _objConn.Close();
            return phoneID;
        }
        public SqlDataReader InstallerInfo(int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetInstallers";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void NewInstaller(int VendorID, string InstallerName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddInstaller";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@InstallerName", InstallerName);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public int CheckInstallers(int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT COUNT(InstallerID) FROM tblInstallers WHERE VendorID = " + VendorID;
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public SqlDataReader StoresByVendor(int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetStoresByVendor";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader VendorListByStore(int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pVendorListByStore";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader AssignedVendors(int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetVendorsAssigned";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader AvailableVendors(int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetVendorToAssigne";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void AssignVendorToStore(int StoreID, int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AssignVendor";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void RemoveAssignedVendor(int StoreID, int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_RemoveAssignedVendor";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void AddErrorLog(string PageName, string ProcName, string Message)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddErrorLog";
            objComm.Parameters.AddWithValue("@PageName", PageName);
            objComm.Parameters.AddWithValue("@ProcName", ProcName);
            objComm.Parameters.AddWithValue("@Message", Message);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
    }

    public class OrderData
    {
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConnString);

        public SqlDataReader OrderDetails(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetOrderDetails";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader NewOrderDetails(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetOrderDetails";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader TestOrderDetails(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_TestOrderDetails";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader DeletedDetails(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_DeletedDetails";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public int SaveImage(int OrderID, string FileName, byte[] Image, int FileLength)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddImage";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@ImageName", FileName);
            objComm.Parameters.AddWithValue("@ImageData", Image);
            objComm.Parameters.AddWithValue("@Length", FileLength);
            _objConn.Open();
            int rowsAffected = objComm.ExecuteNonQuery();
            _objConn.Close();
            return rowsAffected;
        }
        public SqlDataReader ImageList(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetImageList";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ShowImage(int ImageID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetImage";
            objComm.Parameters.AddWithValue("@ImageID", ImageID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader StoreList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetAllStores";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader AllStores()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT StoreID, StoreCode + ' - ' + RTRIM(StoreName) + ', ' + State As Store,[State] FROM tblStores ORDER BY [State] ";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void UpdateStore(int OrderID, int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "UPDATE tblOrders SET StoreID = @StoreID WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader ReportStores()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_ReportStores";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader VendorList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetAllVendors";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ReportVendors()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_ReportVendors";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader VendorsAtHome()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT VendorID,VendorName FROM tblVendors WHERE [State] = 'TX' UNION SELECT 0,'' ORDER BY VendorName";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader MarketList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetAllMarkets";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ReportMarkets()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_ReportMarkets";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader MarketByRegion(int RegionID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pMarketsByRegion";
            objComm.Parameters.AddWithValue("@RegionID", RegionID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader StateList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetStateList";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public int NewOrder(string OrderNumb, int CustomerID, int StoreID, DateTime OrderDate,
                            string Planner, string InstallPref, bool DeliveryOption, bool Demolition,
                            char ScopeofDemo, decimal PurchasePrice, decimal BaseInstallPrice,
                            decimal DeliveryPrice, decimal DemoPrice, decimal OrderPrice,
                            string SolutionDescr, string Comments, decimal Actual)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddNewOrder";
            objComm.Parameters.AddWithValue("@OrderNumb", OrderNumb);
            objComm.Parameters.AddWithValue("@CustomerID", CustomerID);
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            objComm.Parameters.AddWithValue("@OrderDate", OrderDate);
            objComm.Parameters.AddWithValue("@Planner", Planner);
            objComm.Parameters.AddWithValue("@InstallPref", InstallPref);
            objComm.Parameters.AddWithValue("@DeliveryOption", DeliveryOption);
            objComm.Parameters.AddWithValue("@Demolition", Demolition);
            objComm.Parameters.AddWithValue("@ScopeofDemo", ScopeofDemo);
            objComm.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            objComm.Parameters.AddWithValue("@SolutionDescr", SolutionDescr);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Actual", Actual);
            SqlParameter parmResult = objComm.Parameters.Add("@OrderID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            int result = int.Parse(objComm.Parameters["@OrderID"].Value.ToString());
            _objConn.Close();
            return result;
        }
        public int NewOrderTCS(string OrderNumb, int CustomerID, int StoreID, DateTime OrderDate,
                            string Planner, string InstallPref, bool DeliveryOption, bool Demolition,
                            char ScopeofDemo, decimal PurchasePrice, decimal BaseInstallPrice,
                            decimal DeliveryPrice, decimal DemoPrice, decimal OrderPrice,
                            DateTime InstallDate, string InstallTime, string Duration,
                            string SolutionDescr, string Comments, decimal Actual)
        {
            int result;
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddNewTCSOrder";
            objComm.Parameters.AddWithValue("@OrderNumb", OrderNumb);
            objComm.Parameters.AddWithValue("@CustomerID", CustomerID);
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            objComm.Parameters.AddWithValue("@OrderDate", OrderDate);
            objComm.Parameters.AddWithValue("@Planner", Planner);
            objComm.Parameters.AddWithValue("@InstallPref", InstallPref);
            objComm.Parameters.AddWithValue("@DeliveryOption", DeliveryOption);
            objComm.Parameters.AddWithValue("@Demolition", Demolition);
            objComm.Parameters.AddWithValue("@ScopeofDemo", ScopeofDemo);
            objComm.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            objComm.Parameters.AddWithValue("@InstallDate", InstallDate);
            objComm.Parameters.AddWithValue("@InstallTime", InstallTime);
            objComm.Parameters.AddWithValue("@Duration", Duration);
            objComm.Parameters.AddWithValue("@SolutionDescr", SolutionDescr);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Actual", Actual);
            SqlParameter parmResult = objComm.Parameters.Add("@OrderID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
                result = int.Parse(objComm.Parameters["@OrderID"].Value.ToString());

            }
            finally
            {
                _objConn.Close();
            }
            return result;
        }
        public int NewOrderTCSTest(string OrderNumb, int CustomerID, int StoreID, DateTime OrderDate,
                            string Planner, string InstallPref, bool DeliveryOption, bool Demolition,
                            char ScopeofDemo, decimal PurchasePrice, decimal BaseInstallPrice,
                            decimal DeliveryPrice, decimal DemoPrice, decimal OrderPrice,
                            DateTime InstallDate, string InstallTime, string Duration,
                            string SolutionDescr, string Comments, decimal Actual)
        {
            int result;
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddNewTCSOrderTest";
            objComm.Parameters.AddWithValue("@OrderNumb", OrderNumb);
            objComm.Parameters.AddWithValue("@CustomerID", CustomerID);
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            objComm.Parameters.AddWithValue("@OrderDate", OrderDate);
            objComm.Parameters.AddWithValue("@Planner", Planner);
            objComm.Parameters.AddWithValue("@InstallPref", InstallPref);
            objComm.Parameters.AddWithValue("@DeliveryOption", DeliveryOption);
            objComm.Parameters.AddWithValue("@Demolition", Demolition);
            objComm.Parameters.AddWithValue("@ScopeofDemo", ScopeofDemo);
            objComm.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            objComm.Parameters.AddWithValue("@InstallDate", InstallDate);
            objComm.Parameters.AddWithValue("@InstallTime", InstallTime);
            objComm.Parameters.AddWithValue("@Duration", Duration);
            objComm.Parameters.AddWithValue("@SolutionDescr", SolutionDescr);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Actual", Actual);
            SqlParameter parmResult = objComm.Parameters.Add("@OrderID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
                result = int.Parse(objComm.Parameters["@OrderID"].Value.ToString());

            }
            finally
            {
                _objConn.Close();
            }
            return result;
        }
        public int NewCustomer(string FName, string Mi, string LName, string Address1, string Address2,
                               string City, string State, string Zip, string Hphone, string Phone2, string Email)
        {
            int result;
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddNewCustomer";
            objComm.Parameters.AddWithValue("@fName", FName);
            objComm.Parameters.AddWithValue("@mi", Mi);
            objComm.Parameters.AddWithValue("@lName", LName);
            objComm.Parameters.AddWithValue("@address1", Address1);
            objComm.Parameters.AddWithValue("@address2", Address2);
            objComm.Parameters.AddWithValue("@city", City);
            objComm.Parameters.AddWithValue("@state", State);
            objComm.Parameters.AddWithValue("@zip", Zip);
            objComm.Parameters.AddWithValue("@hphone", Hphone);
            objComm.Parameters.AddWithValue("@phone2", Phone2);
            objComm.Parameters.AddWithValue("@email", Email);
            SqlParameter parmResult = objComm.Parameters.Add("@CustomerID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
                result = int.Parse(objComm.Parameters["@CustomerID"].Value.ToString());
            }
            finally
            {
                _objConn.Close();
            }
            return result;
        }
        public int NewCustomerTest(string FName, string Mi, string LName, string Address1, string Address2,
                               string City, string State, string Zip, string Hphone, string Phone2, string Email)
        {
            int result;
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddNewCustomerTest";
            objComm.Parameters.AddWithValue("@fName", FName);
            objComm.Parameters.AddWithValue("@mi", Mi);
            objComm.Parameters.AddWithValue("@lName", LName);
            objComm.Parameters.AddWithValue("@address1", Address1);
            objComm.Parameters.AddWithValue("@address2", Address2);
            objComm.Parameters.AddWithValue("@city", City);
            objComm.Parameters.AddWithValue("@state", State);
            objComm.Parameters.AddWithValue("@zip", Zip);
            objComm.Parameters.AddWithValue("@hphone", Hphone);
            objComm.Parameters.AddWithValue("@phone2", Phone2);
            objComm.Parameters.AddWithValue("@email", Email);
            SqlParameter parmResult = objComm.Parameters.Add("@CustomerID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
                result = int.Parse(objComm.Parameters["@CustomerID"].Value.ToString());
            }
            finally
            {
                _objConn.Close();
            }
            return result;
        }
        public void UpdateCustomer(int OrderID, string Hphone, string Phone2, string Email, string InstNotes, string InvNotes)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateCustomer";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@hphone", Hphone);
            objComm.Parameters.AddWithValue("@phone2", Phone2);
            objComm.Parameters.AddWithValue("@email", Email);
            objComm.Parameters.AddWithValue("@InstNotes", InstNotes);
            objComm.Parameters.AddWithValue("@InvoiceNotes", InvNotes);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateAddress(int OrderID, string Address1, string Address2, string City, string State,string Zip,string Hphone)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateAddress";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@address1", Address1);
            objComm.Parameters.AddWithValue("@address2", Address2);
            objComm.Parameters.AddWithValue("@city", City);
            objComm.Parameters.AddWithValue("@state", State);
            objComm.Parameters.AddWithValue("@zip", Zip);
            objComm.Parameters.AddWithValue("@hphone", Hphone);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateInstallAddress(int OrderID, string Address1, string Address2, string City, string State,string Zip)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sUpdateCustomerAddress";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@address1", Address1);
            objComm.Parameters.AddWithValue("@address2", Address2);
            objComm.Parameters.AddWithValue("@city", City);
            objComm.Parameters.AddWithValue("@state", State);
            objComm.Parameters.AddWithValue("@zip", Zip);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateInstallName(int OrderID,string FirstName,string Mi,string LastName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateCustomerName";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@FirstName", FirstName);
            objComm.Parameters.AddWithValue("@MI", Mi);
            objComm.Parameters.AddWithValue("@LastName", LastName);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateResponse(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateResponse";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateOrder(int OrderID, int VendorID, bool PickedUp, bool Installed,
                                DateTime? InstallDate, DateTime? PaymentDate, int Status,
                                string SolutionDescr, string Comments, decimal BaseInstallPrice, decimal DeliveryPrice,
                                decimal DemoPrice, decimal MilesPrice, decimal MiscPrice, decimal OrderPrice,
                                string InstallTime, string Duration, bool Approved)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateOrder";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@PickedUp", PickedUp);
            objComm.Parameters.AddWithValue("@Installed", Installed);
            objComm.Parameters.AddWithValue("@InstallDate", InstallDate);
            objComm.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            objComm.Parameters.AddWithValue("@Status", Status);
            objComm.Parameters.AddWithValue("@SolutionDescr", SolutionDescr);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@MilesPrice", MilesPrice);
            objComm.Parameters.AddWithValue("@MiscPrice", MiscPrice);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            objComm.Parameters.AddWithValue("@InstallTime", InstallTime);
            objComm.Parameters.AddWithValue("@Duration", Duration);
            objComm.Parameters.AddWithValue("@Approved", Approved);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void NewUpdateOrder(int OrderID, bool PickedUp, bool Installed,
                                DateTime? PaymentDate, int Status,
                                string SolutionDescr, string Comments, decimal BaseInstallPrice, decimal DeliveryPrice,
                                decimal DemoPrice, decimal MilesPrice, decimal MiscPrice, decimal Parking, decimal TipPrice, 
                                decimal PromoPrice,decimal Tax, decimal OrderPrice,bool Approved, string CheckNumb, bool BySurvey)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateOrder";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@PickedUp", PickedUp);
            objComm.Parameters.AddWithValue("@Installed", Installed);
            objComm.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            objComm.Parameters.AddWithValue("@Status", Status);
            objComm.Parameters.AddWithValue("@SolutionDescr", SolutionDescr);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@MilesPrice", MilesPrice);
            objComm.Parameters.AddWithValue("@MiscPrice", MiscPrice);
            objComm.Parameters.AddWithValue("@ParkingPrice", Parking);
            objComm.Parameters.AddWithValue("@TipPrice", TipPrice);
            objComm.Parameters.AddWithValue("@PromoPrice", PromoPrice);
            objComm.Parameters.AddWithValue("@Tax", Tax);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            objComm.Parameters.AddWithValue("@Approved", Approved);
            objComm.Parameters.AddWithValue("@CheckNumb", CheckNumb);
            objComm.Parameters.AddWithValue("@BySurvey", BySurvey);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void AddOfficeComment(int OrderID,string Comments)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAddOfficeComments";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
            }
            finally
            {
                _objConn.Close();
            }
        }
        public void NewAppointment(int OrderID, DateTime StartTime, DateTime EndTime, string Notes)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAddAppointment";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@StartTime", StartTime);
            objComm.Parameters.AddWithValue("@EndTime", EndTime);
            objComm.Parameters.AddWithValue("@Notes", Notes);
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
            }
            finally
            {
                _objConn.Close();
            }
        }
        public void NewAppointmentTest(int OrderID, DateTime StartTime, DateTime EndTime, string Notes)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAddAppointmentTest";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@StartTime", StartTime);
            objComm.Parameters.AddWithValue("@EndTime", EndTime);
            objComm.Parameters.AddWithValue("@Notes", Notes);
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
            }
            finally
            {
                _objConn.Close();
            }
        }
        public void UpdateAppointment(int OrderID, DateTime StartTime, DateTime EndTime)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateAppointment";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@StartTime", StartTime);
            objComm.Parameters.AddWithValue("@EndTime", EndTime);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void SaveOrderAttribute(int OrderID, string Attribute)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAddAttribute";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Attribute", Attribute);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void SaveInstallation(string OrderNumb, DateTime StartTime, DateTime EndTime, string Notes, int OrderID)
        {
            var objComm = new SqlCommand
            {
                Connection = _objConn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "pSetInstallation"
            };
            objComm.Parameters.AddWithValue("@OrderNumb", OrderNumb);
            objComm.Parameters.AddWithValue("@StartTime", StartTime);
            objComm.Parameters.AddWithValue("@EndTime", EndTime);
            objComm.Parameters.AddWithValue("@Notes", Notes);
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void AssignVendor(int OrderID, int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pSetVendor";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void CloseAppointment(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pCloseAppointment";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void CleanAppointment(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pCleanAppointment";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void UpdateInstaller(int OrderID, string Installer)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateInstaller";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Installer", Installer);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void CleanImages(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_CleanImages";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public SqlDataReader OrderSearch(string SParameter)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_OrderSearch";
            objComm.Parameters.AddWithValue("@sParam", SParameter);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public int OrdersbyStore(int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_OrdersByStore";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public int OrdersbyVendor(int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_OrdersByVendor";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public void DeleteOrder(int OrderID, string Comments, string UserName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_DeleteOrder";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@UserName", UserName);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void DeleteOrderTest(int OrderID, string Comments)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_DeleteOrderTest";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void UpdateBasePrice(int OrderID, decimal PurchasePrice, decimal BaseInstallPrice,
                                    decimal DeliveryPrice, decimal DemoPrice, decimal OrderPrice)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateBasePrice";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void UpdateOrderPrice(int OrderID, decimal BaseInstallPrice, decimal DeliveryPrice,
                                     decimal DemoPrice, decimal MilesPrice, decimal MiscPrice, decimal TipPrice, decimal PromoPrice,
                                     decimal OrderPrice, int PayType, decimal VendorDue, DateTime VendorDate, int Status)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateOrderPrice";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@MilesPrice", MilesPrice);
            objComm.Parameters.AddWithValue("@MiscPrice", MiscPrice);
            objComm.Parameters.AddWithValue("@TipPrice", TipPrice);
            objComm.Parameters.AddWithValue("@PromoPrice", PromoPrice);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            objComm.Parameters.AddWithValue("@PayType", PayType);
            objComm.Parameters.AddWithValue("@VendorDue", VendorDue);
            objComm.Parameters.AddWithValue("@VendorDate", VendorDate);
            objComm.Parameters.AddWithValue("@Status", Status);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateVendorDue(int OrderID, decimal VendorDue, DateTime VendorDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateVendorDue";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@VendorDue", VendorDue);
            objComm.Parameters.AddWithValue("@VendorDate", VendorDate);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader ShowYTDvalues()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_YTDreport";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ShowYTDfiscal()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_YTDfiscal";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ShowFiscalByDate(DateTime StartDate, DateTime EndDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_FiscalByDate";
            objComm.Parameters.AddWithValue("@StartDate", StartDate);
            objComm.Parameters.AddWithValue("@EndDate", EndDate);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader TimeList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT * FROM tblTime ORDER BY RecordID";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader TimeList2()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT * FROM tblTime WHERE Active=1 ORDER BY RecordID";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader DurationList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT * FROM tblDurations ORDER BY DurationID";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader DurationList2()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT * FROM tblDurations WHERE active=1 ORDER BY DurationID";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader YTDbyStore(int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_YTDbyStore";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader TCSbyStoreF(int StoreID, DateTime StartDate, DateTime EndDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_TCSbyStoreF";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            objComm.Parameters.AddWithValue("@StartDate", StartDate);
            objComm.Parameters.AddWithValue("@EndDate", EndDate);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader YTDbyStoreF(int StoreID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_YTDbyStoreF";
            objComm.Parameters.AddWithValue("@StoreID", StoreID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader GetCallsInfo(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetCallsInfo";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void SaveCallsInfo(int OrderID, int CallID, DateTime CallDate, string Status)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_SaveCallInfo";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@CallID", CallID);
            objComm.Parameters.AddWithValue("@Date", CallDate);
            objComm.Parameters.AddWithValue("@Result", Status);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public SqlDataReader FiscalDates()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "Select StartDate,EndDate FROM tblCalendar Where EndDate<GETDATE() AND fYear>2007 ORDER BY StartDate DESC";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader TDSCompletedByDate(DateTime StartDate, DateTime EndDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_TCS_Completed_byDate";
            objComm.Parameters.AddWithValue("@StartDate", StartDate);
            objComm.Parameters.AddWithValue("@EndDate", EndDate);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader LastFiscalDates(string StartDate, string EndDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetLastFiscal";
            objComm.Parameters.AddWithValue("@StartDate", StartDate);
            objComm.Parameters.AddWithValue("@EndDate", EndDate);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader InstallSchedule(int VendorID, DateTime InstallDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_InstallSchedule";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@InstallDate", InstallDate);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void AddTroubleLog(int OrderID, string Description, string Trip, string Contact, 
            string Missing, string Damaged, string User, int ReasonCode)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddTroubleLog";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Description", Description);
            objComm.Parameters.AddWithValue("@Trip", Trip);
            objComm.Parameters.AddWithValue("@Contact", Contact);
            objComm.Parameters.AddWithValue("@Missing", Missing);
            objComm.Parameters.AddWithValue("@Damaged", Damaged);
            objComm.Parameters.AddWithValue("@User", User);
            objComm.Parameters.AddWithValue("@ReasonCode", ReasonCode);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public SqlDataReader GetOrderProblems(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetOrderProblems";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader GetTroubleLog(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetTroubleLog";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void AddFeedBack(int OrderID, string InstHours, string InstMin, string DemolHours,
                                string DemolMin, string IfMissing, string IfDamaged, string IfSlow, string TxtComments)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddFeedBack";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@InstHours", InstHours);
            objComm.Parameters.AddWithValue("@InstMin", InstMin);
            objComm.Parameters.AddWithValue("@DemolHours", DemolHours);
            objComm.Parameters.AddWithValue("@DemolMin", DemolMin);
            objComm.Parameters.AddWithValue("@IfMissing", IfMissing);
            objComm.Parameters.AddWithValue("@IfDamaged", IfDamaged);
            objComm.Parameters.AddWithValue("@IfSlow", IfSlow);
            objComm.Parameters.AddWithValue("@txtComments", TxtComments);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public int CheckNoSchedule(int VendorID, DateTime? CheckDate)
        {
            int result = 0;
            if (CheckDate != null)
            {
                var objComm = new SqlCommand();
                objComm.Connection = _objConn;
                objComm.CommandType = CommandType.StoredProcedure;
                objComm.CommandText = "sp_CheckNoSchedule";
                objComm.Parameters.AddWithValue("@VendorID", VendorID);
                objComm.Parameters.AddWithValue("@CheckDate", CheckDate);
                _objConn.Open();
                result = int.Parse(objComm.ExecuteScalar().ToString());
                _objConn.Close();
            }
            return result;
        }
        public int CheckScheduledDay(int VendorID, DateTime CheckDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_CheckScheduledDate";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@CheckDate", CheckDate);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public void AddOffDate(int VendorID, DateTime OffDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddOffDate";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@OffDate", OffDate);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void AddVendorBonus(int VendorID, int OrderID, decimal Amount)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddVendorBonus";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Amount", Amount);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void AddOrganizerBonus(int OrganizerID, int OrderID, decimal Amount)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAddOrganizerBonus";
            objComm.Parameters.AddWithValue("@OrganizerID", OrganizerID);
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Amount", Amount);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public SqlDataReader GetAdditionalVendor(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_CheckAddVendor";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader GetAdditionalOrganizer(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pCheckAddOrganizer";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader GetAdditionalPayment(int OrderID,int VendorID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pCheckAddPayment";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void AddMissing(int OrderID, string Details, string Resolution, string Employee,
                               string Delivered, string Paid)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddMissing";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Details", Details);
            objComm.Parameters.AddWithValue("@Resolution", Resolution);
            objComm.Parameters.AddWithValue("@Employee", Employee);
            objComm.Parameters.AddWithValue("@Delivered", Delivered);
            objComm.Parameters.AddWithValue("@Paid", Paid);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void AddDamaged(int OrderID, string Details, string Resolution, string Employee,
                               string Delivered, string Paid)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_AddDamaged";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Details", Details);
            objComm.Parameters.AddWithValue("@Resolution", Resolution);
            objComm.Parameters.AddWithValue("@Employee", Employee);
            objComm.Parameters.AddWithValue("@Delivered", Delivered);
            objComm.Parameters.AddWithValue("@Paid", Paid);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void SaveLogOut(int UserID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_SaveLogOut";
            objComm.Parameters.AddWithValue("@UserID", UserID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public int GetStoreIDbyCode(string Store)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "DECLARE @StoreID int SET @StoreID=0 " +
                                  "SELECT @StoreID=StoreID FROM tblStores WHERE StoreCode='" + Store + "' " +
                                  "SELECT @StoreID";
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }

        public string GetInstallerPhone(int VendorID)
        {
            string result = "";
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT TOP 1 phone FROM tblVphones WHERE VendorID= @VendorID AND phone!='' ORDER BY PhoneID";
            objComm.Parameters.AddWithValue("@VendorID", VendorID);
            _objConn.Open();
            try
            {
                result = objComm.ExecuteScalar().ToString();
            }
             catch
             {
                 result = "N/A";
             }
            finally
            {
                _objConn.Close();
                
            }
            return result;
        }

        public int CheckOrder(string OrderNumb, string LName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_CheckOrder";
            objComm.Parameters.AddWithValue("@OrderNumb", OrderNumb);
            objComm.Parameters.AddWithValue("@LastName", LName);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }
        public int CheckOrderTest(string OrderNumb, string LName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_CheckOrderTest";
            objComm.Parameters.AddWithValue("@OrderNumb", OrderNumb);
            objComm.Parameters.AddWithValue("@LastName", LName);
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }

        public void UpdateOrderTCSInfo(int OrderID, string InstallPref, bool DeliveryOption,
                                    bool Demolition, char ScopeofDemo, decimal PurchasePrice, decimal BaseInstallPrice,
                                    decimal DeliveryPrice, decimal DemoPrice, decimal OrderPrice,
                                    DateTime InstallDate, string InstallTime, string Duration,
                                    string SolutionDescr, string Comments, decimal Actual)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateDupeTCSOrder";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@InstallPref", InstallPref);
            objComm.Parameters.AddWithValue("@DeliveryOption", DeliveryOption);
            objComm.Parameters.AddWithValue("@Demolition", Demolition);
            objComm.Parameters.AddWithValue("@ScopeofDemo", ScopeofDemo);
            objComm.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            objComm.Parameters.AddWithValue("@InstallDate", InstallDate);
            objComm.Parameters.AddWithValue("@InstallTime", InstallTime);
            objComm.Parameters.AddWithValue("@Duration", Duration);
            objComm.Parameters.AddWithValue("@SolutionDescr", SolutionDescr);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Actual", Actual);
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
            }
            finally
            {
                _objConn.Close();
            }
        }
        public void UpdateOrderTCSInfoTest(int OrderID, string InstallPref, bool DeliveryOption,
                                    bool Demolition, char ScopeofDemo, decimal PurchasePrice, decimal BaseInstallPrice,
                                    decimal DeliveryPrice, decimal DemoPrice, decimal OrderPrice,
                                    DateTime InstallDate, string InstallTime, string Duration,
                                    string SolutionDescr, string Comments, decimal Actual)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_UpdateDupeTCSOrderTest";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@InstallPref", InstallPref);
            objComm.Parameters.AddWithValue("@DeliveryOption", DeliveryOption);
            objComm.Parameters.AddWithValue("@Demolition", Demolition);
            objComm.Parameters.AddWithValue("@ScopeofDemo", ScopeofDemo);
            objComm.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
            objComm.Parameters.AddWithValue("@BaseInstallPrice", BaseInstallPrice);
            objComm.Parameters.AddWithValue("@DeliveryPrice", DeliveryPrice);
            objComm.Parameters.AddWithValue("@DemoPrice", DemoPrice);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            objComm.Parameters.AddWithValue("@InstallDate", InstallDate);
            objComm.Parameters.AddWithValue("@InstallTime", InstallTime);
            objComm.Parameters.AddWithValue("@Duration", Duration);
            objComm.Parameters.AddWithValue("@SolutionDescr", SolutionDescr);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Actual", Actual);
            _objConn.Open();
            try
            {
                objComm.ExecuteNonQuery();
            }
            finally
            {
                _objConn.Close();
            }
        }

        public void UpdateOption(int OrderID, string Option)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateOption";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Option", Option);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public int GetNextNumber()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pNextNumber";
            _objConn.Open();
            int result = int.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }

        public decimal GetTaxRate(string ZipCode, string City)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetTaxRate";
            objComm.Parameters.AddWithValue("@ZipCode", ZipCode);
            objComm.Parameters.AddWithValue("@City", City);
            _objConn.Open();
            decimal result = decimal.Parse(objComm.ExecuteScalar().ToString());
            _objConn.Close();
            return result;
        }

        public void UpdateTax(int OrderID,decimal TaxAmount,decimal OrderPrice)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandText = "UPDATE tblOrders SET Tax = @TaxAmount, OrderPrice = @OrderPrice WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@TaxAmount", TaxAmount);
            objComm.Parameters.AddWithValue("@OrderPrice", OrderPrice);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateAccountComm(int OrderID, string Comments)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAccountComm";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateImageView(int OrderID, bool ImgView)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateImgView";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@ImgView", ImgView);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateInvoiceNotes(int CustomerID, string InvoiceNotes)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateInvoiceNotes";
            objComm.Parameters.AddWithValue("@CustomerID", CustomerID);
            objComm.Parameters.AddWithValue("@InvoiceNotes", InvoiceNotes);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdatePaymentType(int OrderID,int PaymentType)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdatePaymentType";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@PaymentType", PaymentType);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader GetColors ()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT ColorID,ColorName FROM tblColors UNION SELECT 0,'' ORDER BY 1 ";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void AddSpace(int OrderID,string SpaceName,string SpaceNumber,bool Texture,string Description,
            int Color, string NonElfa, string Instruction, string Removal, string ColorName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAddSpace";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@SpaceName", SpaceName);
            objComm.Parameters.AddWithValue("@SpaceNumber", SpaceNumber);
            objComm.Parameters.AddWithValue("@Texture", Texture);
            objComm.Parameters.AddWithValue("@Description", Description);
            objComm.Parameters.AddWithValue("@Color", Color);
            objComm.Parameters.AddWithValue("@NonElfa", NonElfa);
            objComm.Parameters.AddWithValue("@Instruction", Instruction);
            objComm.Parameters.AddWithValue("@Removal", Removal);
            objComm.Parameters.AddWithValue("@ColorName", ColorName);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateSpace(int OrderID,int SpaceID,string SpaceName,string SpaceNumber,bool Texture,string Description,
            int Color, string NonElfa, string Instruction, string Removal, string ColorName)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateSpace";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@SpaceID", SpaceID);
            objComm.Parameters.AddWithValue("@SpaceName", SpaceName);
            objComm.Parameters.AddWithValue("@SpaceNumber", SpaceNumber);
            objComm.Parameters.AddWithValue("@Texture", Texture);
            objComm.Parameters.AddWithValue("@Description", Description);
            objComm.Parameters.AddWithValue("@Color", Color);
            objComm.Parameters.AddWithValue("@NonElfa", NonElfa);
            objComm.Parameters.AddWithValue("@Instruction", Instruction);
            objComm.Parameters.AddWithValue("@Removal", Removal);
            objComm.Parameters.AddWithValue("@ColorName", ColorName);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader GetSpacesInfo(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetSpacesInfo";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void RemoveSpace(int OrderID,int SpaceID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "DELETE FROM tblSpaces WHERE OrderID = @OrderID AND SpaceID = @SpaceID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@SpaceID", SpaceID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateSpaceHeader(int OrderID, DateTime? DateStart, DateTime? DateEnd, string InstNotes)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateSpaceHeader";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@DateStart", DateStart);
            objComm.Parameters.AddWithValue("@DateEnd", DateEnd);
            objComm.Parameters.AddWithValue("@InstNotes", InstNotes);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader ShowSpecialists()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT SpecialistID,([Name]+', '+Phone) As Specialist FROM tblSpecialists WHERE Active=1 UNION SELECT 0,'' ORDER BY 1";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader GetOrganizers()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetAllOrganizers";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader OrganizerInfo(int OrganizerID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetOrganizerInfo";
            objComm.Parameters.AddWithValue("@OrganizerID", OrganizerID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int NewOrganizer(string OrganizerName, string Phone, string City, string Address, string State,
                string Zip, string Email, string Comments, decimal Procent, bool Deduct, string ContractorNumb)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAddNewOrganizer";
            objComm.Parameters.AddWithValue("@OrganizerName", OrganizerName);
            objComm.Parameters.AddWithValue("@Phone", Phone);
            objComm.Parameters.AddWithValue("@City", City);
            objComm.Parameters.AddWithValue("@Address", Address);
            objComm.Parameters.AddWithValue("@State", State);
            objComm.Parameters.AddWithValue("@Zip", Zip);
            objComm.Parameters.AddWithValue("@Email", Email);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Procent", Procent);
            objComm.Parameters.AddWithValue("@Deduct", Deduct);
            objComm.Parameters.AddWithValue("@ContractorNumb", ContractorNumb);
            SqlParameter parmResult = objComm.Parameters.Add("@OrganizerID", SqlDbType.Int);
            parmResult.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            int vendorID = int.Parse(objComm.Parameters["@OrganizerID"].Value.ToString());
            _objConn.Close();
            return vendorID;
        }

        public void UpdateOrganizer(int OrganizerID,string OrganizerName, string Phone, string City, string Address, string State,
                string Zip, string Email, string Comments, bool Active, decimal Procent, bool Deduct, string ContractorNumb)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateOrganizer";
            objComm.Parameters.AddWithValue("@OrganizerID", OrganizerID);
            objComm.Parameters.AddWithValue("@OrganizerName", OrganizerName);
            objComm.Parameters.AddWithValue("@Phone", Phone);
            objComm.Parameters.AddWithValue("@City", City);
            objComm.Parameters.AddWithValue("@Address", Address);
            objComm.Parameters.AddWithValue("@State", State);
            objComm.Parameters.AddWithValue("@Zip", Zip);
            objComm.Parameters.AddWithValue("@Email", Email);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@Active", Active);
            objComm.Parameters.AddWithValue("@Procent", Procent);
            objComm.Parameters.AddWithValue("@Deduct", Deduct);
            objComm.Parameters.AddWithValue("@ContractorNumb", ContractorNumb);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader GetAthomeData(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT * FROM tblAtHome WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void SaveAtHome(int OrderID,int SpecialistID,string PickUp,string Location,
            string Staging,DateTime? StagingDate,string Styling,DateTime? StylingDate,
            int Vendor1ID, int Vendor2ID, string Proc1, string Proc2, string Special, bool Completed, 
            decimal AddServices, decimal NonElfaCost)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pSaveAtHome";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@SpecialistID", SpecialistID);
            objComm.Parameters.AddWithValue("@PickUp", PickUp);
            objComm.Parameters.AddWithValue("@Location", Location);
            objComm.Parameters.AddWithValue("@Staging", Staging);
            objComm.Parameters.AddWithValue("@StagingDate", StagingDate);
            objComm.Parameters.AddWithValue("@Styling", Styling);
            objComm.Parameters.AddWithValue("@StylingDate", StylingDate);
            objComm.Parameters.AddWithValue("@Vendor1ID", Vendor1ID);
            objComm.Parameters.AddWithValue("@Vendor2ID", Vendor2ID);
            objComm.Parameters.AddWithValue("@Proc1", Proc1);
            objComm.Parameters.AddWithValue("@Proc2", Proc2);
            objComm.Parameters.AddWithValue("@Special", Special);
            objComm.Parameters.AddWithValue("@Completed", Completed);
            objComm.Parameters.AddWithValue("@AddServices", AddServices);
            objComm.Parameters.AddWithValue("@NonElfaCost", NonElfaCost);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader ShowAtHomeData(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pShowAtHomeData";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void CopyAtHomeData(int OldOrderID, int NewOrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pCopyAtHome";
            objComm.Parameters.AddWithValue("@OldOrderID", OldOrderID);
            objComm.Parameters.AddWithValue("@NewOrderID", NewOrderID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader ShowExemptList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandText = "SELECT ExemptID,Description FROM tblTaxExempt";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void SetTaxExempt(int OrderID, int ExemptID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandText = "UPDATE tblOrders SET ExemptID = @ExemptID WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@ExemptID", ExemptID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void SetConfirmedDate(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandText = "UPDATE tblOrders SET ApproveDate = CAST(GETDATE() As DATE) WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public SqlDataReader ShowDiscountReasons()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandText = "SELECT DiscountID,DiscountReason FROM tblDiscount WHERE Active=1";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ShowInstalledDiscount()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandText = "SELECT DiscountID,DiscountReason FROM tblDiscount WHERE Active=1 OR DiscountID>15 ORDER BY 1";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void SaveDiscount(int OrderID, int DiscountID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandText = "UPDATE tblOrders SET DiscountID = @DiscountID WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@DiscountID", DiscountID);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public string DiscountReason(int DiscountID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT DiscountReason FROM tblDiscount WHERE DiscountID = @DiscountID";
            objComm.Parameters.AddWithValue("@DiscountID", DiscountID);
            _objConn.Open();
            string result = objComm.ExecuteScalar().ToString().Trim();
            _objConn.Close();
            return result;
        }
        public SqlDataReader GetOrderRegions(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetOrderRegion";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ShowProblemReasons()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT ReasonCode,Description FROM tblReasonCodes WHERE CallLog=1 OR ReasonCode=0";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ShowFulFillReasons()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT ReasonCode,Description FROM tblReasonCodes WHERE FulFill=1 OR ReasonCode=0";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader ShowServiceReasons()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT ReasonCode,Description FROM tblReasonCodes WHERE Service=1 OR ReasonCode=0";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public SqlDataReader RegionAttention(int RegionID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pRegionAttention";
            objComm.Parameters.AddWithValue("@RegionID", RegionID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void UpdateTOrder(int OrderID, string OrderNumb, string Comments)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "UPDATE tblOrders SET OrderNumb = @OrderNumb, Comments = @Comments WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@OrderNumb", OrderNumb);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateSpecialStatus(int OrderID,int StatusID,DateTime StatusDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pSpecialStatus";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@StatusID", StatusID);
            objComm.Parameters.AddWithValue("@StatusDate", StatusDate);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void SaveReason(int OrderID,int ReasonCode)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "INSERT INTO tblReasons (OrderID,ReasonCode) VALUES (@OrderID,@ReasonCode)";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@ReasonCode", ReasonCode);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void SaveAdditionalDemo(int OrderID,decimal AddService)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "UPDATE tblOrders SET DemoPrice = DemoPrice + @AddService WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@AddService", AddService);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void AddOrganizerInvoice(int OrderID, int OrganizerID, string Comments)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pAddOrganizerInvoice";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@OrganizerID", OrganizerID);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public SqlDataReader ShowOrganizerInvoice(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pShowOrganizerInvoice";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader ShowOrganizerOrder(int OrderID)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT * FROM tblOrganizerInvoice WHERE OrderID = @OrderID";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void UpdateOrganizerInvoice(int OrderID,int OrganizerID,decimal Fees,decimal Other,decimal Adjustment,string Comments)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateOrganizerInvoice";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@OrganizerID", OrganizerID);
            objComm.Parameters.AddWithValue("@Fees", Fees);
            objComm.Parameters.AddWithValue("@Other", Other);
            objComm.Parameters.AddWithValue("@Adjustment", Adjustment);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
        public void UpdateOrganizerOrder(int OrderID,int Status,int PayType,
            string CheckNumb,bool Installed)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateOrganizerOrder";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Status", Status);
            objComm.Parameters.AddWithValue("@PayType", PayType);
            objComm.Parameters.AddWithValue("@CheckNumb", CheckNumb);
            objComm.Parameters.AddWithValue("@Installed", Installed);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateOrganizerPay(int OrderID,DateTime PaymentDate,string Comments,
            decimal OrganizerDue, DateTime OrganizerDate)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "UpdateOrganizerPay";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            objComm.Parameters.AddWithValue("@Comments", Comments);
            objComm.Parameters.AddWithValue("@OrganizerDue", OrganizerDue);
            objComm.Parameters.AddWithValue("@OrganizerDate", OrganizerDate);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }

        public void UpdateDeductFlag(int OrderID, bool Deduct)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pUpdateDeductFlag";
            objComm.Parameters.AddWithValue("@OrderID", OrderID);
            objComm.Parameters.AddWithValue("@Deduct", Deduct);
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
        }
    }

    public class MonthInfo
    {
        public string MonthName;
        public double Invoice;
        public double Payment;
    }
}