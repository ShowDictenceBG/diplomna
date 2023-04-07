using Diplomna.miscellaneous.algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

internal class initialize {

    private static parser   m_parser = new parser( );
    private static globals.node m_nodes = new globals.node( );

    static void Main( string[ ] args ) {


        // set our path.
        m_parser.set_path( "C:\\Users\\bokat\\OneDrive\\Desktop\\test.graphml" );

        // load new xml file.
        m_parser.load_document( m_parser.get_path( ) );
        if ( m_parser.parse( ) ) {

            /* we got our information */
            algorithm algorithm = new algorithm( m_parser.get_nodes_list( ), m_parser.get_topology_sort_list( ) );
            algorithm.main(  );

        }
    }

}