// <auto-generated />
namespace Anketa.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    [GeneratedCode("EntityFramework.Migrations", "6.1.1-30610")]
    public sealed partial class initial : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(initial));

        string IMigrationMetadata.Id
        {
            get { return "201504250539373_initial"; } //return "201509021520479_InitialCreate";  drugi izbor, jedino �to jo� u bazi postoji.
        }

        string IMigrationMetadata.Source
        {
            get { return null; }
        }

        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
