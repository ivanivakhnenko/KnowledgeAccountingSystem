namespace KnowledgeAccountingSystem.BLL.DTO
{
    public class SkillValueDTO
    {
        public int Id { get; set; }
        public SkillDTO Skill { get; set; }
        public int Value { get; set; }
        public CategoryDTO Category { get; set; }
    }
}