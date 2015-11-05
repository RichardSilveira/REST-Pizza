namespace RESTPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedido", "SenhaEspera", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "SenhaEspera", c => c.String());
        }
    }
}
