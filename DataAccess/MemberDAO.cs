using BussinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class MemberDAO : BaseDAL
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<MemberObject> GetMemberList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberId, Email, CompanyName, City, Country, Password from Member";
            var members = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    members.Add(new MemberObject
                    {
                        MemberId = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConneciton();
            }
            return members;
        }

        public MemberObject GetMemberByID(int memberId)
        {
            MemberObject member = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberId, Email, CompanyName, City, Country, Password " +
                "  from Member where MemberId=@MemberId";
            try
            {
                var param = dataProvider.CreateParameter("@MemberId", 4, memberId, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    member = new MemberObject
                    {
                        MemberId = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConneciton();
            }
            return member;
        }

        public void AddNew(MemberObject member)
        {
            try
            {
                MemberObject mem = GetMemberByID(member.MemberId);
                if (mem == null)
                {
                    string SQLInsert = "Insert Member values(@MemberId, @Email, @CompanyName, @City, @Country, @Password)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberId", 4, member.MemberId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@CompanyName", 40, member.CompanyName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 15, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 15, member.Country, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member is already exist. ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConneciton();
            }
        }

        public void Update(MemberObject member)
        {
            try
            {
                MemberObject mem = GetMemberByID(member.MemberId);
                if (mem != null)
                {
                    string SQLUpdate = "Update Cars set Email=@Email, CompanyName=@CompanyName, City=@City, Country=@Country, Password=@Password" +
                        "where MemberId=@MemberId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberId", 4, member.MemberId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@CompanyName", 40, member.CompanyName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 15, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 15, member.Country, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    dataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member does not already exist. ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConneciton();
            }
        }

        public void Remove(int memberId)
        {
            try
            {
                MemberObject mem = GetMemberByID(memberId);
                if (mem != null)
                {
                    string SQLDelete = "Delete Member where MemberId= @MemberId";
                    var param = dataProvider.CreateParameter("@CarID", 4, memberId, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("THe member does not already exist. ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConneciton();
            }
        }

        public MemberObject Login(int memberId, string password)
        {
            MemberObject member = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberId, Email, CompanyName, City, Country, Password " +
                "  from Member where MemberId=@MemberId";
            try
            {
                var param = dataProvider.CreateParameter("@MemberId", 4, memberId, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    if (password.Equals(dataReader.GetString(5)))
                     {
                        member = new MemberObject
                        {
                            MemberId = dataReader.GetInt32(0),
                            Email = dataReader.GetString(1),
                            CompanyName = dataReader.GetString(2),
                            City = dataReader.GetString(3),
                            Country = dataReader.GetString(4),
                            Password = dataReader.GetString(5)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConneciton();
            }
            return member;
        }
    }
}
