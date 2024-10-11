using System;
namespace Models.DTO
{
    public class adminInfoDbDto
    {
        //attraction info
        public int nrSeededAttractions { get; set; } = 0;
        public int nrRemovedAttractions { get; set; } = 0;

        //user info
        public int nrSeededUsers { get; set; } = 0;
        public int nrRemovedUsers { get; set; } = 0;

        //comment info
        public int nrSeededComments { get; set; } = 0;
        public int nrRemovedComments { get; set; } = 0;

        //address info
        public int nrSeededAddresses { get; set; } = 0;
        public int nrRemovedAddresses { get; set; } = 0;
    }
}