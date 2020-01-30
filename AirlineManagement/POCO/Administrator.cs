using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class Administrator: IUser, IPoco
    {
        public Administrator(long iD, string fIRST_NAME, string lAST_NAME, string uSER_NAME, string pASSWORD)
        {
            ID = iD;
            FIRST_NAME = fIRST_NAME;
            LAST_NAME = lAST_NAME;
            USER_NAME = uSER_NAME;
            PASSWORD = pASSWORD;
        }

        public Administrator()
        {
        }

        public long ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }

        public static bool operator ==(Administrator thisAdmin, Administrator otherAdmin)
        {
            if (ReferenceEquals(thisAdmin, null) && ReferenceEquals(otherAdmin, null))
                return true;
            if (ReferenceEquals(thisAdmin, null) || ReferenceEquals(otherAdmin, null))
                return false;
            return thisAdmin.ID == otherAdmin.ID;
        }
        public static bool operator !=(Administrator thisAdmin, Administrator otherAdmin)
        {
            if (thisAdmin == null && otherAdmin == null)
                return false;
            if (thisAdmin == null || otherAdmin == null)
                return true;
            return !(thisAdmin.ID == otherAdmin.ID);
        }

        public override bool Equals(object obj)
        {
            var admin = obj as Administrator;
            if (ReferenceEquals(admin, null))
                return false;

            return this.ID == admin.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
