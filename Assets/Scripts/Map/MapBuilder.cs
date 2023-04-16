using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Builds the Map at Start.
/// When the Player reaches near the End of the Map, the Map will be extended in height and lower, non visible Objects will be destroyed.
/// This Step will be repeated when the player reaches again the end of the Map.
/// </summary>
public class MapBuilder : MonoBehaviour
{
    /// <summary>
    /// List of Start Templates Game Objects. Used for the First Step.
    /// </summary>
    public GameObject[] StartTemplates { get; private set; }
    /// <summary>
    /// List of Templates Game Objects that are generated suring the game.
    /// </summary>
    public GameObject[] Templates { get; private set; }
    /// <summary>
    /// List of already instatiated Templates.
    /// </summary>
    public List<ITemplate> instantiatedTemplates = new List<ITemplate>();
    /// <summary>
    /// Character Game Object.
    /// </summary>
    private GameObject character;
    /// <summary>
    /// Current Map Height. 
    /// Up to this Height <see cref="Instantiable"/> Objects were created.
    /// Above this height there are no <see cref="Instantiable"/> Objects!
    /// </summary>
    private static uint mapHeight;
    /// <summary>
    /// <para>This is used to generate the Map.
    /// It will be multiplied with <see cref="generatedHeight"/>.</para>
    /// 
    /// <para>After each Map generation Step, this field will be interated by one.
    /// By this the maximum generated height will raised.</para>
    /// </summary>
    private static uint iteration = 1;
    /// <summary>
    /// This is the Height by which the map will be raised each generation interation.
    /// Must be Higher or Equal to 50.
    /// </summary>
    private static readonly uint GENERATED_HEIGHT = 50;
    /// <summary>
    /// Getter for <see cref="GENERATED_HEIGHT"/>.
    /// </summary>
    /// <value>Value of <see cref="GENERATED_HEIGHT"/>.</value>
    public static uint get_GENERATED_HEIGHT
    {
        get { return GENERATED_HEIGHT; }
    }
    /// <summary>
    /// <para>This Field triggers the next Map Generation.
    /// If the Player is higher than the maxHeight minus <see cref="HEIGHT_GENERATING"/> new Map elements will be created.</para>
    /// 
    /// <para>Value must be half or lower than <see cref="GENERATED_HEIGHT"/>.</para>
    /// </summary>
    private static readonly uint HEIGHT_GENERATING = 25;
    /// <summary>
    /// Getter for <see cref="HEIGHT_GENERATING"/>.
    /// </summary>
    /// <value>Value of <see cref="HEIGHT_GENERATING"/>.</value>
    public static uint get_HEIGHT_GENERATING
    {
        get { return HEIGHT_GENERATING; }
    }
    /// <summary>
    /// <para>If the map generating is triggered all Objects that are located under 
    /// the players Height - this Field Value will be destroyed.</para>
    /// 
    /// <para>e.G. Players Height is 100 and this field value is 30.
    /// All Objects with a Height lower then 100-30=70 will be destroyed.</para>
    /// 
    /// <para>Value must be half or lower than <see cref="GENERATED_HEIGHT"/>.</para>
    /// </summary>
    private static readonly uint HEIGHT_DESTROING = 25;
    /// <summary>
    /// Getter for <see cref="HEIGHT_DESTROING"/>.
    /// </summary>
    /// <value>Value of <see cref="HEIGHT_DESTROING"/>.</value>
    public static uint get_HEIGHT_DESTROING
    {
        get { return HEIGHT_GENERATING; }
    }
    /// <summary>
    /// Creates a random number. This Number is used to get a random element out of the Template Lists/ Arrays.
    /// </summary>
    /// <returns>Object to create random Numbers.</returns>
    private static System.Random rnd = new System.Random();
    /// <summary>
    /// Mode for which the MapBuilder should create the Map.
    /// </summary>
    /// <value>MapBuilder mode</value>
    public MapBuildingModes mode = MapBuildingModes.PLAY;
    /// <summary>
    /// Mode for which the MapBuilder should create the Map.
    /// </summary>
    /// <value>MapBuilder mode</value>
    public MapBuildingModes Mode
    {
        get { return mode; }
        set { mode = value; }
    }

    /// <summary>
    /// Uses a random Start Template Game Object out of <see cref="StartTemplates"/> to generat the Start Pont of the Game.
    /// </summary>
    /// <returns>Heigt of the start Template and max Height of the Game Map.</returns>
    private uint GenerateStartTemplates()
    {
        int elementInt = rnd.Next(StartTemplates.Length);
        return GenerateTemplate(StartTemplates[elementInt]);
    }

    private uint GenerateTemplates(GameObject[] templateGameObjects)
    {
        uint maxHeight = GENERATED_HEIGHT * iteration;
        for (; mapHeight < maxHeight;)
        {
            int elementInt = rnd.Next(templateGameObjects.Length);
            mapHeight = GenerateTemplate(Instantiate(templateGameObjects[elementInt]));
        }

        iteration++;
        return mapHeight;
    }

    private void GenerateTemplatesToTest(GameObject[] templateGameObjects)
    {
        Util.FisherYatesShuffle(templateGameObjects);

        foreach(GameObject templateGameObject in templateGameObjects){
            GenerateTemplate(Instantiate(templateGameObject));
        }
    }

    /// <summary>
    /// Generates the Instantiables of a template Game Object.
    /// </summary>
    /// <param name="templateGameObject">The Game Object that contains a Template Component.</param>
    /// <returns>New Max Height of the Map.</returns>
    private uint GenerateTemplate(GameObject templateGameObject)
    {
        ITemplate template = templateGameObject.GetComponent<ITemplate>();
        //Save instatiated Templates
        Debug.Log(System.String.Format("Create Template at vertikal Position:{0}.", mapHeight));
        template.generateMap(mapHeight);
        instantiatedTemplates.Add(template);
        mapHeight = mapHeight + template.GetElementHeight();

        return mapHeight;
    }

    /// <summary>
    /// Destroy all <see cref="Instantiable"/> of the Elements this method receives.
    /// </summary>
    /// <param name="instToDestroy">List of <see cref="ITemplate"/>. All elemnts Instantiables will be destroyed.</param>
    private void DestroyTemplates(List<ITemplate> instToDestroy)
    {
        for (int i = instToDestroy.Count - 1; i >= 0; i--)
        {
            Debug.Log(System.String.Format("Destroy Element at vertikal Position:{0}.", instToDestroy[i].GetElementPositionHeight()));
            instToDestroy[i].DestroyAllInstantiables();
            instToDestroy.RemoveAt(i);
        }
    }

    /// <summary>
    /// <para>Generates the first Map elements.</para>
    /// 
    /// <para>Start with one Random element out of <see cref="StartTemplates"/>.</para>
    /// 
    /// <para>After that random templates out of <see cref="Templates"/> will be used
    ///  until the map height is <see cref="iteration"/>*<see cref="generatedHeight"/>.
    ///  Iteration will be raised by 1.</para>
    /// </summary>
    public void Start()
    {
        StartTemplates = GetTemplateGameObjects("StartTemplatesPrefab");
        Templates = GetTemplateGameObjects("TemplatesOnePrefab");

        character = GameObject.FindWithTag(TagEnum.Player);
        mapHeight = 0;

        switch (mode)
        {
            case MapBuildingModes.START_TEST:
                GenerateTemplatesToTest(StartTemplates);
                break;
            case MapBuildingModes.LEVEL_ONE_TEST:
                GenerateTemplatesToTest(Templates);
                break;
            case MapBuildingModes.PLAY:
            default:
                mapHeight = GenerateStartTemplates();
                GenerateTemplates(Templates);

                Resume();
                break;
        }
    }

    /// <summary>
    /// Gets Pause Manager of Scene.
    /// Calls Resume of the Pause Manager.
    /// If Game gets restarted after Pause this sets the Time of the Game back to normal.
    /// </summary>
    private void Resume()
    {
        GameObject[] pauseManagerGos = GameObject.FindGameObjectsWithTag(TagEnum.PAUSE_MANAGER_TAG);
        GameObject pauseManagerGo = pauseManagerGos[0];
        PauseManager pauseManager = pauseManagerGo.GetComponent<PauseManager>();
        pauseManager.Resume();
    }

    /// <summary>
    /// Get all Game Objects from a Resource Folder.
    /// This Folder should contain only template Game Objects.
    /// </summary>
    /// <param name="folderPath">Name of the Folder inside the Resource Folder.</param>
    /// <returns>Array of found GameObjects</returns>
    private GameObject[] GetTemplateGameObjects(string folderPath)
    {
        GameObject[] gos = Resources.LoadAll<GameObject>(folderPath);
        if (gos.Length <= 0)
        {
            Debug.Log(System.String.Format("Cannot find any Prefab in Folder:{0}", folderPath));
            return new GameObject[0];
        }

        return gos;
    }

    /// <summary>
    /// Checks if the Character Height is higher then <see cref="mapHeight"/> - <see cref="HEIGHT_GENERATING"/>.
    /// If so new map elements will be generated,
    ///  until <see cref="mapHeight"/> is greater then <see cref="generatedHeight"/> + <see cref="iteration"/>.
    /// </summary>
    public void Update()
    {
        Vector3 characterPosition = character.transform.position;
        float characterHeight = characterPosition.y;

        if (characterHeight > mapHeight - HEIGHT_GENERATING)
        {
            Debug.Log(System.String.Format("Generate new Map Elements."));
            GenerateTemplates(Templates);
            List<ITemplate> instToDestroy = instantiatedTemplates
                .FindAll(instantiatedTemplate => instantiatedTemplate.GetElementPositionHeight() < (characterHeight - HEIGHT_DESTROING));
            DestroyTemplates(instToDestroy);
        }
    }
}
