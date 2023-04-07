using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomna.miscellaneous.algorithm
{
    internal class algorithm {

        /* Store our list of nodes. */
        private List< globals.node > nodes= new List< globals.node >( );
        private List< int > topology_sort = new List< int >( );

        /* apply when intence created. */
        public algorithm( List< globals.node > nodes, List< int > topology_sort ) {
            this.nodes = nodes;
            this.topology_sort = topology_sort;
        }

        public void main(  )  {

            for (int i = 0; i < nodes.Count; i++) {
                Console.WriteLine( nodes[i].get_id( ) );

            }

            for (int i = 0; i < topology_sort.Count; i++) {
                Console.WriteLine( nodes[i].get_id( ) );

            }

        }
    }
}
