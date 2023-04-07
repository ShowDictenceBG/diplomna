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
    private XmlNamespaceManager nsmgr;
    private List< globals.node > m_nodes = new List<globals.node>( );
    private globals.graph graph = new globals.graph( );
    private List< int > topology_sort = new List<int>( );

    public parser( ) {

        // setup on parser create.
        this.document = new XmlDocument( );


        nsmgr = new XmlNamespaceManager( document.NameTable );
        nsmgr.AddNamespace("main", "http://graphml.graphdrawing.org/xmlns");
        nsmgr.AddNamespace("style", "http://www.yworks.com/yFilesHTML/demos/FlatDemoStyle/1.0");

    }

    public void load_document( string path ) { 

        // load our document.
        this.document.Load( path );

    }

    private XmlNodeList get_nodes( ) { 

        // create instence.
        XmlNodeList current_nodes = this.document.SelectNodes( "/main:graphml/main:graph/main:node" , nsmgr );

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

    public bool parse() {

        if (this.get_nodes() == null)
        { return false; }

        graph.initialize( this.get_nodes( ).Count );

        /* node */

        XmlNodeList nodes = this.document.SelectNodes( "/main:graphml/main:graph/main:node" , nsmgr );


        /* loop thru all nodes. */
        for ( int i = 0; i < nodes.Count; i++ ) {
                 
            /* get current node. */ 
            XmlNode node = nodes[ i ];
            if ( node == null ) { 
                continue;
            }

            // get our node id.
            XmlAttribute node_id = get_attribute( node, "id" );
            m_nodes.Add( new node( node_id.Value, "", "" ) );
        }

        // create instence.
        XmlNodeList types = this.document.SelectNodes( "/main:graphml/main:graph/main:node/main:data/style:FlowchartNodeStyle" , nsmgr );
        for ( int i = 0; i < types.Count; i++ ) {

            /* get current FlowchartNodeStyle. */
            XmlNode flowchart_node_style = types[ i ];
            if ( flowchart_node_style == null ) { 
                continue;
            }

            // get our node id.
            XmlAttribute flowchart_node_style_type = get_attribute( flowchart_node_style, "type" );
            m_nodes[ i ].set_type( flowchart_node_style_type.Value );
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

        graph.write_graph( );
        topology_sort = graph.sort(  );
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

    public List< globals.node > get_nodes_list( ) {
        return m_nodes;
    }

    public List< int > get_topology_sort_list( ) {
        return topology_sort;
    }
}

