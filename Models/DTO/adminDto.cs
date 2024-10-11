using System;
namespace Models.DTO
{
    public class adminInfoDbDto
    {
        //attraction info
        public int nrSeededAttractions { get; set; } = 0;
        public int nrUnseededAttractions { get; set; } = 0;
        public int nrRemovedAttractions { get; set; } = 0;
        
        //comment info
        public int nrRemovedComments { get; set; } = 0;

        //user info
        public int nrRemovedUsers { get; set; } = 0;
    }
}