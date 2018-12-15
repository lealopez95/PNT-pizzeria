namespace pizeria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false),
                        importe = c.Double(nullable: false),
                        direccion = c.String(),
                        telefono = c.String(),
                        estado = c.Int(nullable: false),
                        sucursal_ID = c.Int(),
                        usuario_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sucursals", t => t.sucursal_ID)
                .ForeignKey("dbo.Usuarios", t => t.usuario_ID)
                .Index(t => t.sucursal_ID)
                .Index(t => t.usuario_ID);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Categoria = c.Int(nullable: false),
                        Nombre = c.String(),
                        Precio = c.Double(nullable: false),
                        Descripcion = c.String(),
                        UrlImagen = c.String(),
                        Pedido_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_ID)
                .Index(t => t.Pedido_ID);
            
            CreateTable(
                "dbo.Sucursals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Direccion = c.String(),
                        administrador_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuarios", t => t.administrador_ID)
                .Index(t => t.administrador_ID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NombreApellido = c.String(),
                        Email = c.String(),
                        Direccion = c.String(),
                        Password = c.String(),
                        ConfirmacionPassword = c.String(),
                        Telefono = c.String(),
                        esAdministrador = c.Boolean(nullable: false),
                        IDSucursalAdministrada = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "usuario_ID", "dbo.Usuarios");
            DropForeignKey("dbo.Pedidoes", "sucursal_ID", "dbo.Sucursals");
            DropForeignKey("dbo.Sucursals", "administrador_ID", "dbo.Usuarios");
            DropForeignKey("dbo.Productoes", "Pedido_ID", "dbo.Pedidoes");
            DropIndex("dbo.Sucursals", new[] { "administrador_ID" });
            DropIndex("dbo.Productoes", new[] { "Pedido_ID" });
            DropIndex("dbo.Pedidoes", new[] { "usuario_ID" });
            DropIndex("dbo.Pedidoes", new[] { "sucursal_ID" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Sucursals");
            DropTable("dbo.Productoes");
            DropTable("dbo.Pedidoes");
        }
    }
}
