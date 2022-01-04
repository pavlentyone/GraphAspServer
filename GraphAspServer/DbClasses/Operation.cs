using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbClasses
{
    // Справочник кодов операций с поездом
    [Table("NSI_TRAIN_OPERATIONS")]
    public class Operation
    {
        [Column("HID")]
        public int Hid { get; set; }

        // Цифровой код операции
        [Column("OP_CODE")]
        public string Code { get; set; }

        // Цифровой подкод операции
        [Column("OP_EXCODE")]
        public string Subcode { get; set; }

        // Буквенный код операции (мнемокод)
        [Column("OP_MCODE")]
        public string CodeName { get; set; }

        // Наименование операции
        [Column("OP_NAME")]
        public string Name { get; set; }

        // id операции
        [Key, Column("OP_ID")]
        public int Id { get; set; }
    } 
}
