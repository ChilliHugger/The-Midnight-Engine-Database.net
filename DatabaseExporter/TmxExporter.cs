using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;

// ReSharper disable StringLiteralTypo
namespace DatabaseExporter
{
    /*
     * <tileset firstgid="1209" name="Items" tilewidth="64" tileheight="64" tilecount="8" columns="8">
     *    <image source="map_items.png" width="512" height="64"/>
     * </tileset>
     */
    public class TmxExporter
    {
        private readonly IEntityContainer _entityContainer;
        private string _folder = "";
        private XmlWriter _writer;
        private Dictionary<string, int> _symbols = new();
        private int _groupId = 10;

        public TmxExporter(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        public void Process(string folder, string scenario)
        {
            _folder = folder;
            
            // build label lookup map
            var id = 1;
            var symbols1 = _entityContainer.RouteNodes.ToDictionary(e => e.Symbol, e => id++);
            var symbols2 = _entityContainer.Waypoints.ToDictionary(e => e.Symbol, e => id++);
            var symbols3 = _entityContainer.Strongholds.ToDictionary(e => e.Symbol, e => id++);
            var symbols4 = _entityContainer.Regiments.ToDictionary(e => e.Symbol, e => id++);
            var symbols5 = _entityContainer.Things.ToDictionary(e => e.Symbol, e => id++);
            var symbols6 = _entityContainer.Lords.ToDictionary(e => e.Symbol, e => id++);
            _symbols = Merge(symbols1, symbols2, symbols3, symbols4, symbols5, symbols6);

            
            var settings = new XmlWriterSettings
            {
                Indent = true
                
            };

            _writer = XmlWriter.Create(Path.Combine(_folder,scenario) + ".tmx", settings);
            _writer.WriteStartDocument();
            _writer.WriteStartElement("map");

            
            // Items
            RouteNodes();
            Waypoints();
            Strongholds();
            Regiments();
            Objects();
            Characters();
            
            _writer.WriteEndElement();
            _writer.WriteEndDocument();
            _writer.Close();
        }
        
        
        //  <objectgroup id="10" name="routenodes"></objectgroup>
        /*
           <object id="211" name="RN_KOR_KEEP_37" type="routenode" x="2560" y="1536" width="64" height="64">
            <properties>
             <property name="LEFT" type="object" value="184"/>
             <property name="RIGHT" type="object" value="184"/>
            </properties>
           </object>
         */
        private void RouteNodes()
        {
            _writer.WriteStartElement("objectgroup");
            _writer.WriteAttributeString("id", _groupId.ToString());
            _writer.WriteAttributeString("name", "routenodes");
            _groupId++;
            
            foreach (var r in _entityContainer.RouteNodes)
            {
                _writer.WriteStartElement("object");
                WriteObjectAttributes("routenode", 1213, r);
                
                _writer.WriteStartElement("properties");
                    WriteNode("LEFT", r.RouteNodes.Nodes.First()?.Symbol);
                    WriteNode("RIGHT", r.RouteNodes.Nodes.Last()?.Symbol);
                _writer.WriteEndElement();
                
                _writer.WriteEndElement();
            }
            _writer.WriteEndElement();
        }

        private void WriteObjectGroup<T>(string group, string type, int gid, IEnumerable<T> items)
            where T : IItem
        {
            _writer.WriteStartElement("objectgroup");
            _writer.WriteAttributeString("id", _groupId.ToString());
            _writer.WriteAttributeString("name", group);
            _groupId++;
            foreach (var item in items)
            {
                _writer.WriteStartElement("object");
                WriteObjectAttributes(type, gid, item);
                _writer.WriteEndElement();
            }
            _writer.WriteEndElement();
        }
        
        private void WriteObjectAttributes<T>(string type, int gid, T entity) 
            where T : IItem
        {
            _writer.WriteAttributeString("type", type);
            _writer.WriteAttributeString("id", _symbols[entity.Symbol].ToString());
            _writer.WriteAttributeString("name", entity.Symbol);
            _writer.WriteAttributeString("width", "64");
            _writer.WriteAttributeString("height", "64");
            _writer.WriteAttributeString("x", (entity.Location.X * 64).ToString());
            _writer.WriteAttributeString("y", (entity.Location.Y * 64).ToString());
            _writer.WriteAttributeString("gid", gid.ToString());
        }

        private void WriteNode(string name, string value)
        {
            _writer.WriteStartElement("property");
                _writer.WriteAttributeString("name", name);
                _writer.WriteAttributeString("type", "object");
                _writer.WriteAttributeString("value", _symbols[value].ToString());
            _writer.WriteEndElement();
        }
        
        private void Waypoints()
        {
            WriteObjectGroup("waypoints", "waypoint", 1211, _entityContainer.Waypoints);
        }
        
        private void Strongholds()
        {
            WriteObjectGroup("strongholds", "stronghold", 1210, _entityContainer.Strongholds);
        }
        
        private void Regiments()
        {
            _writer.WriteStartElement("objectgroup");
            _writer.WriteAttributeString("id", _groupId.ToString());
            _writer.WriteAttributeString("name", "regiments");
            _groupId++;
            
            foreach (var r in _entityContainer.Regiments)
            {
                _writer.WriteStartElement("object");
                WriteObjectAttributes("regiment", 1209, r);
                
                _writer.WriteStartElement("properties");
                WriteNode("TARGET", r.Target?.Symbol);
                _writer.WriteEndElement();
                
                _writer.WriteEndElement();
            }
            _writer.WriteEndElement();
            // WriteObjectGroup("regiments", "regiment", 1209, _entityContainer.Regiments);
        }
        
        private void Objects()
        {
            WriteObjectGroup("objects", "object", 1214, _entityContainer.Things);
        }

        private void Characters()
        {
            WriteObjectGroup("characters", "character", 1212, _entityContainer.Lords);
        }
        
        
        private Dictionary<TKey, TValue> Merge<TKey, TValue>(
            params Dictionary<TKey, TValue>[] dictionaries)
        {
            var keys = new List<TKey>();
            var values = new List<TValue>();
            foreach (var dictionary in dictionaries)
            {
                foreach (var kvp in dictionary)
                {
                    keys.Add(kvp.Key);
                    values.Add(kvp.Value);
                }
            }
            var mergedDictionary = keys.Zip(values, (key, value) => new KeyValuePair<TKey, TValue>(key, value))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return mergedDictionary;
        }

    }
}

