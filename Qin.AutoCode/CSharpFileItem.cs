using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qin.AutoCode
{
    public class CSharpFileItem
    {
        public string Name { get; set; }
        public string[] References { get; set; }
        public string Namespace { get; set; }
        public List<ClassItem> Classes { get; set; }
    }





    public class ClassItem
    {
        public ClassItem(string name, string inherit = "", string modifier = "public")
        {
            Modifier = modifier;
            Name = name;
            Inherit = inherit;
        }

        public string Modifier { get; set; }
        public string Name { get; set; }
        public string Inherit { get; set; }
        public List<PropertyItem> Properties { get; set; }
        public List<FiledItem> Fields { get; set; }
        public List<InjectItem> Injects { get; set; }
        public List<MethodItem> Methods { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            string strInherit = string.IsNullOrEmpty(Inherit) ? "" : $" : {Inherit}";
            sb.AppendLine($"\t{Modifier} class {Name}{strInherit}");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\t\\\\AutoCode Generate.");

            if (Injects?.Count > 0)
            {
                Injects?.ForEach(d => sb.AppendLine(d.ToString()));
                // need add constructor
            }

            Fields?.ForEach(d => sb.AppendLine(d.ToString()));
            Properties?.ForEach(d => sb.AppendLine(d.ToString()));
            Methods?.ForEach(d => sb.AppendLine(d.ToString()));

            sb.AppendLine("\t}");



            return sb.ToString();
        }
    }

    public abstract class ItemBase
    {
        public ItemBase(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }
        public string Type { get; set; }

        public override abstract string ToString();
    }

    public class PropertyItem : ItemBase
    {
        public PropertyItem(string name, string type) : base(name, type)
        {
        }

        public override string ToString()
        {
            return $"public {Type} {Name} {{ get; set; }}";
        }
    }

    public class FiledItem : ItemBase
    {
        public FiledItem(string name, string type, string modifier = "private") : base(name, type)
        {
            this.Modifier = modifier;
        }

        public string Modifier { get; set; }
        public override string ToString()
        {
            return $"{Modifier} {Type} {Name};";
        }
    }

    public class InjectItem : ItemBase
    {

        public InjectItem(string name, string type) : base(name, type)
        {
        }

        public override string ToString()
        {
            return $"private readonly {Type} {Name};";
        }
    }

    public class MethodItem : ItemBase
    {
        public string Modifier { get; set; }
        public MethodItem(string name, string type = "void", string modifier = "public") : base(name, type)
        {
            this.Modifier = modifier;
        }

        public List<ParameterItem> Parameters { get; set; }

        public List<string> MethodLines { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var param = Parameters.Select(d => d.ToString()).ToList();
            var methodTitle = $"{Modifier} {Type} {Name}({string.Join(',', param)})".Trim();
            sb.AppendLine($"\t\t{methodTitle}");
            sb.AppendLine("\t\t{");
            MethodLines?.ForEach(d => sb.AppendLine(d));
            sb.AppendLine("\t\t}");

            return sb.ToString();
        }
    }
    public class ParameterItem : ItemBase
    {
        public ParameterItem(string type, string name, string modifier = "", string defaultValue) : base(name, type)
        {
            this.Type = type;
            this.Name = name;
            this.Modifier = modifier;
            this.DefaultValue = defaultValue;
        }
        public string Modifier { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; } = "";
        public override string ToString()
        {
            if (string.IsNullOrEmpty(DefaultValue))
            {

                return $"{Modifier} {Type} {Name}".Trim();
            }
            else
            {
                return $"{Modifier} {Type} {Name} = {DefaultValue}".Trim();
            }
        }
    }

}
