using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;

internal class globals {



    public class node {

        // store our values.
        private string id = "#undefined";
        private string type = "#undefined";
        private string cdata = "#undefined";

        public node( ) {
 
        }

        public node( string id, string type, string cdata ) {
        
            this.id = id;
            this.type = type;
            this.cdata= cdata;
        }

        public void set_id( string id ) { 
            this.id = id;
        }

        public string get_id( ) { 
            return this.id;
        }

        public void set_type( string type ) { 
            this.type = type;   
        }

        public string get_type( ) { 
            return this.type;
        }

        public void set_cdata( string cdata) { 
            this.cdata = cdata;   
        }

        public string get_cdata( ) { 
            return this.cdata;
        }
    }

    // create edge_graph
    public class graph { 
    
        private List< node > m_nodes;
        private int[ , ] matrix;

        public graph(  ) { 
        
        }

        public bool initialize( int size ) {

            matrix = new int[ size, size ];
            if ( matrix == null ) {
                Console.WriteLine( "Object creation failed!" );
                return false;
            }

            return true;
        }

        public bool add_edge( int src, int target ) {

            matrix[ src, target ] = 1;
            return true;
        }

        public bool initialzie_nodes( List< node > nodes ) {

            // make sure its not null.
            if ( nodes == null ) {
                Console.WriteLine( "Object is null!" );
                return false;
            }

            // set our nodes.
            m_nodes = nodes;

            return true;
        }

        public void write_graph( ) {

            // get row & col length.
            int rowLength = matrix.GetLength( 0 );
            int colLength = matrix.GetLength( 1 );


            // loop thru row.
            for ( int i = 0; i < rowLength; i++ ) {

                // loop thru col
                for ( int j = 0; j < colLength; j++ ) {

                    // print depending on both row & col.
                    Console.Write( string.Format( "{0} ", matrix[ i, j ] ) );
                }

                // new line.
                Console.WriteLine( );
            }
        }
    }
}
