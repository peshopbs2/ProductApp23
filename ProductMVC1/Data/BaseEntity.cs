using Microsoft.AspNetCore.Identity;

namespace ProductMVC1.Data
{
    public abstract class BaseEntity
    {
        
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string? CreatedById { get; set; }
        public virtual IdentityUser? CreatedBy { get; set; }
        public string? ModifiedById { get; set; }
        public virtual IdentityUser? ModifiedBy { get; set; }
    }
}
