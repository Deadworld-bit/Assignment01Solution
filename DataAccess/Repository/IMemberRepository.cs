using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMemberByID(int memberId);
        void InsertMember(MemberObject member);
        void DeleteMember(int memberId);
        void UpdateMember(MemberObject member);
        MemberObject Login(int memberId, string password);
    }
}
