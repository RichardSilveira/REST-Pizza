namespace RESTPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoObjetos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        PedidoID = c.Int(nullable: false, identity: true),
                        SenhaEspera = c.String(),
                        TempoEstimado = c.Decimal(precision: 18, scale: 2),
                        Situacao = c.Int(nullable: false),
                        PizzaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PedidoID)
                .ForeignKey("dbo.Pizza", t => t.PizzaID, cascadeDelete: true)
                .Index(t => t.PizzaID);
            
            CreateTable(
                "dbo.Pizza",
                c => new
                    {
                        PizzaID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Ingredientes = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PizzaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "PizzaID", "dbo.Pizza");
            DropIndex("dbo.Pedido", new[] { "PizzaID" });
            DropTable("dbo.Pizza");
            DropTable("dbo.Pedido");
        }
    }
}
