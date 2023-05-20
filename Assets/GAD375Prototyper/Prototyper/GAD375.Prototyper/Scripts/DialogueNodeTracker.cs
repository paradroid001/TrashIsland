using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
namespace GAD375.Prototyper
{
// Field ... is never assigned to and will always have its default value null
#pragma warning disable 0649

    public class DialogueNodeTracker : MonoBehaviour
    {

        // The dialogue runner that we want to attach the 'visited' function to
#pragma warning disable 0649
    [SerializeField] Yarn.Unity.DialogueRunner dialogueRunner;
#pragma warning restore 0649

        private HashSet<string> _visitedNodes = new HashSet<string>();

        void Awake()
        {
            // Register a function on startup called "visited" that lets Yarn
            // scripts query to see if a node has been run before.
            dialogueRunner.AddFunction("visited", delegate (string nodeName)
            {
                return _visitedNodes.Contains(nodeName);
            });

        }

        // Called by the Dialogue Runner to notify us that a node finished
        // running. 
        public void NodeComplete(string nodeName) {
            // Log that the node has been run.
            _visitedNodes.Add(nodeName);
            //Debug.Log("Node name " + nodeName + " is now complete.");
            //Debug.Log("Completed Set: " + _visitedNodes.ToString() ); 
        }  

        // Called by the Dialogue Runner to notify us that a new node
        // started running. 
        public void NodeStart(string nodeName)
        {
            // Log that the node has been run.

            var tags = new List<string>(dialogueRunner.GetTagsForNode(nodeName));

            //Debug.Log($"Starting the execution of node {nodeName} with {tags.Count} tags.");
        }

        [System.Serializable] class HashStrings
        {
            public List<string> strings;

            public HashStrings()
            {
                strings = new List<string>();
            }
        }
        public string SerializeToJSON(bool prettyPrint=false)
        {
            /*
            List<string> nodes = new List<string>();
            foreach (string item in _visitedNodes)
            {
                nodes.Add(item);
            }
            string savedata = JsonUtility.ToJson(nodes, prettyPrint);
            */
            HashStrings s = new HashStrings();
            foreach (string item in _visitedNodes)
            {
                s.strings.Add(item);
            }
            string savedata = JsonUtility.ToJson(s, prettyPrint);
            return savedata;
        }
        public void DeserializeFromJSON(string json)
        {
            //List<string> nodes = JsonUtility.FromJson<List<string>>(json);
            HashStrings nodes = JsonUtility.FromJson<HashStrings>(json);
            Clear(); //start from scratch
            //_visitedNodes.UnionWith(nodes); //merge the two.
            foreach (string node in nodes.strings)
            {
                _visitedNodes.Add(node);
            }

        }

        public void Clear()
        {
            _visitedNodes.Clear();
        }
    }
}
