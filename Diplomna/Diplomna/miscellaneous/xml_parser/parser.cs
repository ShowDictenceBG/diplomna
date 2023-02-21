using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static globals;
using static System.Net.Mime.MediaTypeNames;

internal class parser {

    /* parser */
    // create instence
    private XmlDocument document;
    private List< globals.node > m_nodes = new List<globals.node>( );
    private globals.graph graph = new globals.graph( );

    public parser( ) {

        // setup on parser create.
        this.document = new XmlDocument( );
    }

    public void load_document( string path ) { 

        // load our document.
        this.document.Load( path );

    }

    private XmlNodeList get_nodes( ) { 

        // create instence.
        XmlNodeList current_nodes = this.document.GetElementsByTagName( "node" );

        // return.
        return current_nodes;
    }

    private XmlNodeList get_edges( ) { 

        // create instence.
        XmlNodeList current_edges = this.document.GetElementsByTagName( "edge" );

        // return.
        return current_edges;
    }

    private XmlAttribute get_attribute( XmlNode node, string key ) {

        // make sure attribute is not null.
        XmlAttribute attribute = node.Attributes[ key ];
        // return our attribute.
        return attribute;
    }

    private XmlAttribute validate_type( XmlAttribute type ) {

        XmlAttribute temp_type = type;
        if ( temp_type.Value != "start1" || temp_type.Value != "decistion" || temp_type.Value != "terminate" ) {
            temp_type = null;
        }

        return temp_type;
    }

    private string format_cdata( string cdata ) {

        if ( cdata == "" ) {
            return "#undefined";
        }

        string[] separators = new string[] { ",", ".", "!", "\'", " ", "\'s" };

        // now we should format our text.
        foreach (string word in cdata.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            Console.WriteLine(word);

        return "#undefined";
    }

    public int find_number( string value ) {

        Regex re = new Regex(@"\d+");
        Match m = re.Match( value );

        if ( m.Success ) {
            return int.Parse( m.Value );
        }

        return -1;
    }

    public bool parse( ) {

        
        graph.initialize( this.get_nodes( ).Count );

        // loop thru all nodes.
        for ( int i = 0; i < this.get_nodes( ).Count; i++ ) {

            // current node.
            XmlNode current_node = this.get_nodes( )[ i ];

            // get our node id.
            XmlAttribute node_id = get_attribute( current_node, "id" );
            XmlAttribute node_type = null;
            string cdata = "";

            // Get type.
            {

                // get all nodes childs.
                XmlNodeList node_child = current_node.ChildNodes;
                for ( int j = 0; j < node_child.Count; j++ ) {
                    
                    // get current child.
                    XmlNode child = node_child[ j ];
                    
                    // make sure its not null.
                    var attribute = child.Attributes[ "key" ];
                    if ( attribute == null ) { 
                        continue; 
                    }

                    // run when we found our d0 key.
                    if ( attribute.Value == "d0" ) {

                        // get the first child. // don't like that method but it works for now.
                        var cdata_value = child.FirstChild.FirstChild.FirstChild.InnerText.Trim( );
                        cdata = cdata_value;    
                        // not for now.
                    }

                    // run when we found our d3 key.
                    if ( attribute.Value == "d3" ) {

                        // save our node_type.
                        node_type = child.FirstChild.Attributes[ "type" ];
                    }
                }
            }

            if ( node_type == null ) {
                continue;
            }


            // add to our list.
            m_nodes.Add( new node( node_id.Value, node_type.Value, cdata ) );
        }

        graph.initialzie_nodes( m_nodes );

        // start getting our edges.
        for ( int i = 0; i < this.get_edges( ).Count; i++ ) {

            // get our child
            XmlNode child = this.get_edges( )[ i ];

            // make sure its not null.
            var attribute_id = child.Attributes[ "id" ];
            if ( attribute_id == null ) {
                continue;
            }

            // set our id.


            // make sure its not null.
            var attribute_source = child.Attributes[ "source" ];
            if ( attribute_source == null ) {
                continue;
            }

            // make sure its not null.
            var attribute_target = child.Attributes[ "target" ];
            if ( attribute_target == null ) {
                continue;
            }

            int source_id = find_number( attribute_source.Value );
            int target_id = find_number( attribute_target.Value );
            graph.add_edge( source_id, target_id );
            
        }

        // graph
        graph.write_graph( );

        return true;
    }
    /* end */


    /* path */
    private string path = "#undefined";

    public string get_path()
    {
        return this.path; // return our path.
    }
    public void set_path(string path)
    {
        this.path = path; // set our path.
    }
    /* end */
}

