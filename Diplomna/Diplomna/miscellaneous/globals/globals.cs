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

            System.Console.WriteLine( );
   
        }

        private int[ , ] copy_matrix( ) {

            // get row & col length.
            int rowLength = matrix.GetLength( 0 );
            int colLength = matrix.GetLength( 1 );

            /* make a copy of our matrix */
            int[ , ] copy = new int[ rowLength, colLength ];
            for ( int i = 0; i < rowLength; i++ ) {

                for ( int j = 0; j < colLength; j++ ) {

                    copy[ i, j ] = matrix[ i, j ];

                }

            }

            return copy;
        }

        private bool check_for_input( int[ , ] matrix, int col_inx ) {

            /* loop thru our nodes. */
            for ( int node = 0; node < matrix.GetLength( 0 ); node++ ) {

                /* ops we have entry nodes. */ 
                if ( matrix[ node, col_inx ] == 1 ) {
                    return false;
                } else /* we don't, thats what we need. */ {
                    return true;
                }

            }

            return false;
        }

        private int[ , ] reset_inbounds( int[ , ] matrix, int col_inx ) {
       
            /* loop thru our nodes. */
            for ( int node = 0; node < matrix.GetLength( 0 ); node++ ) {

                /* if our node is one set to zero. */
                if ( matrix[ node, col_inx ] == 1 ) {
                    matrix[ node, col_inx ] = 0;
                }

            }

            return matrix;
        }


 
        public List< int > sort( ) {

            /* make sure our matrix is valid. */
            if ( this.matrix == null ) {
                return new List<int>( );
            }

            List<int> inx = new List<int>();

            /* make a copy of our matrix */
            int[ , ] copy = copy_matrix( );

            // get row & col length.
            int rowLength = copy.GetLength( 0 );
            int colLength = copy.GetLength( 1 );

            /* keep track what we have visisted. */ 
            bool[] visisted = new bool[ rowLength ];

            /* our steps. */
            for ( int step = 0; step < rowLength; step++ ) {

                /* loop thru our nodes. */
                for ( int node = 0; node < colLength; node++ ) {

                    /* check if our node don't have entry nodes. */
                    if( check_for_input( copy, node ) && visisted[ node ] == false) {

                        /* mark that our node is visited. */
                        visisted[ node ] = true;

                        /* add to our list. */
                        inx.Add( node );

                        /* make all needed changes to our copy matrix. */
                        copy = reset_inbounds(copy, node);

                        /* br */
                        break;
                    }


                }
            }

            return inx;
        }

    }


}