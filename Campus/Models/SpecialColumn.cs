namespace Campus.Models
{



    /// <summary>
    /// 专栏表
    /// </summary>
    public class SpecialColumn
    {
        public int SpecialColumnId { get; set; }

        public string SpecialColumnValue { get; set; }

        public virtual ICollection<Works> Works { get; set; }
    }



}
