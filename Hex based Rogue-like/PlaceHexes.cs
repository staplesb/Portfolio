using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHexes : MonoBehaviour
{
    public GameObject hex;
    public List<Sprite> sprites;
    public Animator animator;
    private List<Vector3> positions;
    private List<List<GameObject>> groups;
    private Vector3 portalStart;
    private Vector3 portalEnd;
    private GameObject enterPortal;
    private GameObject exitPortal;
    
    // Start is called before the first frame update
    private void Awake()
    {
        instMap();

    }

    private void Start()
    {

    }

    private void instMap()
    {
        print("created map");
        positions = new List<Vector3>();
        groups = new List<List<GameObject>>();
        generatePosition(11, 11);
        generateHexes();
        createGroups();
        attachSprites();
        foreach (Transform child in gameObject.transform)
        {

            child.gameObject.GetComponent<Hex>().setAdjacency();
            if (child.position == portalStart)
            {
                enterPortal = child.gameObject;
            }
            else if (child.position == portalEnd)
            {
                exitPortal = child.gameObject;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generatePosition(int width, int height)
    {
        double y = 0;
        double x = 0;
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                x = j;
                if (i % 2 == 1)
                    x += .5;
                positions.Add(new Vector3((float)x, (float)y, 0));

            }
            y += .75;
        }
        portalStart = positions[Mathf.CeilToInt(width / 2)];
        portalEnd = positions[positions.Count - Mathf.CeilToInt(width / 2)-1];
    }

    private void generateHexes()
    {
        foreach (Vector3 position in positions)
        {
            GameObject instance = Instantiate(hex, position, Quaternion.identity);
            instance.transform.parent = gameObject.transform;
        }
    }

    private void createGroups()
    {
        GameObject tempHex;
        List<GameObject> visitedHexes = new List<GameObject>();
        List<GameObject> group = new List<GameObject>();
        foreach (Transform child in gameObject.transform)
        {
            tempHex = child.gameObject;
            if (visitedHexes.Contains(tempHex))
                continue;
            if (group.Count == 0)
            {
                
                group = new List<GameObject>();
                group.Capacity = Random.Range(3, 6);
                group.Add(tempHex);
                visitedHexes.Add(tempHex);
            }
            else if (group[group.Count-1] != null)
            {
                groups.Add(group);
                group = new List<GameObject>();
                group.Capacity = Random.Range(3, 6);
                group.Add(tempHex);
                visitedHexes.Add(tempHex);
            }
            else
            {
                foreach (GameObject hex in tempHex.GetComponent<Hex>().getAdjacent())
                {
                    if (visitedHexes.Contains(hex))
                        continue;
                    if (group.Count == group.Capacity)
                    {
                        groups.Add(group);
                        group = new List<GameObject>();
                        group.Capacity = Random.Range(3, 6);
                        group.Add(hex);
                        visitedHexes.Add(hex);
                    }
                    else
                    {
                        group.Add(hex);
                        visitedHexes.Add(child.gameObject);
                    }
                }
            }

        }
    }

    private void attachSprites()
    {
        Sprite currentSprite;
        float r;
        foreach(List<GameObject> group in groups)
        {
            r = Random.value;
            if (r < .15)
            {
                currentSprite = sprites[3];//water

                
            }
            else if (r < .3)
                currentSprite = sprites[2];
            else if (r < .55)
                currentSprite = sprites[1];
            else
                currentSprite = sprites[0];
            
            foreach(GameObject hex in group)
            {
                hex.GetComponent<SpriteRenderer>().sprite = currentSprite;
                if (currentSprite.Equals(sprites[3]))
                {
                    hex.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/HexAnimator") as RuntimeAnimatorController;
                    hex.GetComponent<Animator>().SetBool("Water", true);
                }

            }
        }

        foreach(Transform child in gameObject.transform)
        {
            if(child.position == portalStart)
            {
                enterPortal = child.gameObject;
                child.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];//portal
                child.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/HexAnimator") as RuntimeAnimatorController;
                child.GetComponent<Animator>().SetBool("Portal", true);
            } else if (child.position == portalEnd)
            {
                exitPortal = child.gameObject;
                child.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
                child.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/HexAnimator") as RuntimeAnimatorController;
                child.GetComponent<Animator>().SetBool("Portal", true);
            }
                
        }


    }

    public void setHexController(GameObject controller)
    {
        foreach(Transform child in gameObject.transform)
        {
            child.gameObject.GetComponent<Hex>().setController(controller);
        }
    }

    public GameObject getEnterPortal(){
        return enterPortal;
    }

    public GameObject getExitPortal()
    {
        return exitPortal;
    }

    public bool searchForPath()
    {
        //GameObject currentHex = gameObject.GetComponent<PlaceHexes>().getEnterPortal();
        List<GameObject> visitedHexes = new List<GameObject>();
        List<GameObject> open = new List<GameObject>();
        open.Add(gameObject.GetComponent<PlaceHexes>().getEnterPortal());

        while (open.Count != 0)
        {
            foreach(GameObject hex in open[0].GetComponent<Hex>().getAdjacent())
            {
                if (!visitedHexes.Contains(hex))
                {
                    if(!hex.GetComponent<SpriteRenderer>().sprite.name.Contains("Mountain") && !hex.GetComponent<SpriteRenderer>().sprite.name.Contains("Water"))
                        open.Add(hex);
                }
            }
            visitedHexes.Add(open[0]);
            open.Remove(open[0]);
            if (open.Contains(exitPortal))
                return true;
          
        }
        if (visitedHexes.Contains(exitPortal))
            return true;
        else
            return false;
    }
}

