namespace FriendShowDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReNameFavorite : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Friend", name: "FavouriteLanguageId", newName: "FavoriteLanguageId");
            RenameIndex(table: "dbo.Friend", name: "IX_FavouriteLanguageId", newName: "IX_FavoriteLanguageId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Friend", name: "IX_FavoriteLanguageId", newName: "IX_FavouriteLanguageId");
            RenameColumn(table: "dbo.Friend", name: "FavoriteLanguageId", newName: "FavouriteLanguageId");
        }
    }
}
