using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _0xc0de_library.INI
{
    public class IniSection
    {
        private readonly IDictionary<string, IniProperty> _properties;


        public IniSection(string name)
        {
            Name = name;
            _properties = new Dictionary<string, IniProperty>();
        
        }


        public string Name { get; set; }

        public string Comment { get; set; }

        public IniProperty[] Properties => _properties.Values.ToArray();


        public string Get(string name)
        {
            if (_properties.ContainsKey(name))
                return _properties[name].Value;

            return null;
        }

        public T Get<T>(string name)
        {
            if (_properties.ContainsKey(name))
                return (T) Convert.ChangeType(_properties[name].Value, typeof(T));

            return default;
        }


        public void Set(string name, string value, string comment = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                RemoveProperty(name);
                return;
            }

            if (!_properties.ContainsKey(name))
            {
                _properties.Add(name, new IniProperty {Name = name, Value = value, Comment = comment});
            }
            else
            {
                _properties[name].Value = value;
                if (comment != null)
                    _properties[name].Comment = comment;
            }
        }

      


        public void RemoveProperty(string propertyName)
        {
            if (_properties.ContainsKey(propertyName))
                _properties.Remove(propertyName);
        }
    }


    public class IniFile
    {
        private readonly IDictionary<string, IniSection> _sections;


        public IniFile()
        {
            _sections = new Dictionary<string, IniSection>();
            CommentChar = '#';
        }

        public IniFile(string path) : this()
        {
            Load(path);
        }


        public IniFile(TextReader reader) : this()
        {
            Load(reader);
        }


        public bool WriteSpacingBetweenNameAndValue { get; set; }

        public char CommentChar { get; set; }

        public IniSection[] Sections => _sections.Values.ToArray();

        private void Load(string path)
        {
            using (var file = new StreamReader(path))
            {
                Load(file);
            }
        }

        private void Load(TextReader reader)
        {
            IniSection section = null;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                // skip empty lines
                if (line == string.Empty)
                    continue;

                // skip comments
                if (line.StartsWith(";") || line.StartsWith("#"))
                    continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    var sectionName = line.Substring(1, line.Length - 2);
                    if (!_sections.ContainsKey(sectionName))
                    {
                        section = new IniSection(sectionName);
                        _sections.Add(sectionName, section);
                    }

                    continue;
                }

                if (section != null)
                {
                    var keyValue = line.Split(new[] {"="}, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (keyValue.Length != 2)
                        continue;

                    section.Set(keyValue[0].Trim(), keyValue[1].Trim());
                }
            }
        }


        public IniSection Section(string sectionName)
        {
            IniSection section;
            if (!_sections.TryGetValue(sectionName, out section))
            {
                section = new IniSection(sectionName);
                _sections.Add(sectionName, section);
            }

            return section;
        }

 
        public void RemoveSection(string sectionName)
        {
            if (_sections.ContainsKey(sectionName))
                _sections.Remove(sectionName);
        }

    
        public void Save(string path)
        {
            using (var file = new StreamWriter(path))
                Save(file);
        }

     
        public void Save(TextWriter writer)
        {
            foreach (var section in _sections.Values)
            {
                if (section.Properties.Length == 0)
                    continue;

                if (section.Comment != null)
                    writer.WriteLine($"{CommentChar} {section.Comment}");

                writer.WriteLine($"[{section.Name}]");

                foreach (var property in section.Properties)
                {
                    if (property.Comment != null)
                        writer.WriteLine($"{CommentChar} {property.Comment}");

                    var format = WriteSpacingBetweenNameAndValue ? "{0} = {1}" : "{0}={1}";
                    writer.WriteLine(format, property.Name, property.Value);
                }

                writer.WriteLine();
            }
        }

       
        public override string ToString()
        {
            using (var sw = new StringWriter())
            {
                Save(sw);
                return sw.ToString();
            }
        }
    }
}