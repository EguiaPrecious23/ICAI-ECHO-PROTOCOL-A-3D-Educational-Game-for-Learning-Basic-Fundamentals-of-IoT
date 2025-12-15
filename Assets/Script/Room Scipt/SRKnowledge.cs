using System.Collections;
using TMPro;
using UnityEngine;

public class SRKnowledge : MonoBehaviour
{
    [Header ("Arduino Audio")]
    public AudioSource srKnowledge;

    [Header("PPT Big Screen")]
    public TMP_Text whiteboardSubtitleText;
    public GameObject whiteboardTextBG;

    [Header("PPT Small Screen")]
    public TMP_Text whiteboardSubtitleText1;
    public GameObject whiteboardTextBG1;

    [Header ("Screenboard")]
    public TMP_Text screenboardText;
    public GameObject screenboardBG;

    [Header("Alien Hologram")]
    public GameObject Holo;
    public GameObject Holo2;

    [Header("Door Lock")]
    public GameObject SRDoor;
    public GameObject MRDoor;

    [Header("Objective")]
    public GameObject objCanvas;
    public TMP_Text fObjText;
    public TMP_Text objText;

    [Header("UI")]
    public GameObject cursor;

    [Header("Market room Light")]
    public GameObject mrLight;

    [Header("Arduino tools camera")]
    public GameObject ArduinoCamera;
    public GameObject MotorCamera;
    public GameObject BoardCamera;
    public GameObject ResistorCamera;
    public GameObject LEDCamera;
    public GameObject WireCamera;
    public GameObject SensorCamera;

    [Header("Spinning Tool")]
    public GameObject spinningArduino;
    public GameObject spinningMotor;
    public GameObject spinningBoard;
    public GameObject spinningResistor;
    public GameObject spinningLED;
    public GameObject spinningWires;
    public GameObject spinningWires2;
    public GameObject spinningSensor;

    [Header("Waypoint")]
    public GameObject waypoint3;
    public GameObject waypoint4;

    [Header("Obj Outline")]
    public GameObject foutline;
    public GameObject foutline2;

    [Header("Ojb Outline")]
    public GameObject soutline;
    public GameObject soutline2;
    public GameObject soutline3;
    public GameObject soutline4;
    public GameObject soutline5;

    private bool hasPlayed = false;
    private bool rewardGiven = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasPlayed)
            {
                hasPlayed = true;
                StartCoroutine(sRKnowledge());
            }
        }
    }

    private IEnumerator sRKnowledge()
    {
        whiteboardTextBG.SetActive(true);
        whiteboardTextBG1.SetActive(true);

        Holo.SetActive(true);
        Holo2.SetActive(true);

        SRDoor.SetActive(false);

        objCanvas.SetActive(false);

        waypoint3.SetActive(false);

        yield return new WaitForSeconds(1f);

        srKnowledge.Play();

        if (whiteboardSubtitleText  && whiteboardTextBG1 != null)
        {
            whiteboardSubtitleText.gameObject.SetActive(true);
            whiteboardSubtitleText1.gameObject.SetActive(true);
            StartCoroutine(SubtitleRoutine());
        }

        yield return new WaitWhile(() => srKnowledge.isPlaying);

        whiteboardSubtitleText.gameObject.SetActive(false);
        whiteboardSubtitleText1.gameObject.SetActive(false);

        whiteboardTextBG.SetActive(false);
        whiteboardTextBG1.SetActive(false);

        SRDoor.SetActive(true);
        MRDoor.SetActive(true);

        objCanvas.SetActive(true);
        fObjText.text = "Third Objective:";
        objText.text = "Proceed to the Market room to buy Arduino tools";

        waypoint4.SetActive(true);

        soutline.SetActive(true);
        soutline2.SetActive(true);
        soutline3.SetActive(true);
        soutline4.SetActive(true);
        soutline5.SetActive(true);

        mrLight.SetActive(true);

        if (!rewardGiven)
        {
            rewardGiven = true;
            Inventory.instance.AddMoney(500);
        }

    }

    private IEnumerator SubtitleRoutine()
    {
        whiteboardSubtitleText.text = "";
        whiteboardSubtitleText1.text = "";

        yield return new WaitForSeconds(2f);

        yield return ShowSubtitle("“The world is connected—but so are secrets...”", 5f);
        yield return ShowSubtitle("", 3f);

        yield return ShowSubtitle("Area 1: General IoT Knowledge", 4.5f);
        yield return ShowSubtitle("Area 1: General IoT Knowledge\r\n - These explain what IoT is, how it works, and its role in the world.", 6f);

        yield return ShowSubtitle("", .75f);
        yield return ShowSubtitle("What is IoT? \r\nInternet of Things is a network of interconnected devices that can send and receive data.", 9f);

        yield return ShowSubtitle("", .5f);
        yield return ShowSubtitle("What is IoT? \r\nInternet of Things is a network of interconnected devices that can send and receive data. \r\nReal-world example: \r\nSmart home appliances, wearables, traffic sensors.", 6f);

        yield return ShowSubtitle("", 2f);
        yield return ShowSubtitle("Core Components of IoT", 4f);

        yield return ShowSubtitle("", 1f);
        yield return ShowSubtitle("Core Components of IoT\r\nSensors & Actuators: Detect and act on real-world data.", 6f);

        yield return ShowSubtitle("", .6f);
        yield return ShowSubtitle("Core Components of IoT\r\nSensors & Actuators: Detect and act on real-world data.\r\nConnectivity: Wi-Fi and Bluetooth transmitting data.\r\n", 7f);

        yield return ShowSubtitle("Core Components of IoT\r\nSensors & Actuators: Detect and act on real-world data.\r\nConnectivity: Wi-Fi and Bluetooth transmitting data.\r\nMicrocontrollers: Like Arduino/Raspberry Pi that process sensor data.", 7f);

        yield return ShowSubtitle("", .5f);
        yield return ShowSubtitle("Core Components of IoT\r\nSensors & Actuators: Detect and act on real-world data.\r\nConnectivity: Wi-Fi and Bluetooth transmitting data.\r\nMicrocontrollers: Like Arduino/Raspberry Pi that process sensor data.\r\nCloud/Data Storage: Where data is sent and analyzed.\r\n", 7.5f);

        yield return ShowSubtitle("", .6f);
        yield return ShowSubtitle("Core Components of IoT\r\nSensors & Actuators: Detect and act on real-world data.\r\nConnectivity: Wi-Fi and Bluetooth transmitting data.\r\nMicrocontrollers: Like Arduino/Raspberry Pi that process sensor data.\r\nCloud/Data Storage: Where data is sent and analyzed.\r\nUser Interface: How humans interact with IoT systems.", 6.4f);

        yield return ShowSubtitle("", 5f);
        yield return ShowSubtitle("Common Applications of IoT", 2.5f);

        yield return ShowSubtitle("Common Applications of IoT\r\nSmart Homes", 1f);

        yield return ShowSubtitle("Common Applications of IoT\r\nSmart Homes\r\nHealth Monitoring", 1.5f);

        yield return ShowSubtitle("", .5f);
        yield return ShowSubtitle("Common Applications of IoT\r\nSmart Homes\r\nHealth Monitoring\r\nAgriculture", 1.5f);

        yield return ShowSubtitle("Common Applications of IoT\r\nSmart Homes\r\nHealth Monitoring\r\nAgriculture\r\nSmart Cities", 1.5f);

        yield return ShowSubtitle("Common Applications of IoT\r\nSmart Homes\r\nHealth Monitoring\r\nAgriculture\r\nSmart Cities\r\nIndustrial IoT", 3f);

        yield return ShowSubtitle("", 2f);

        yield return ShowSubtitle("Area 2: Arduino Uno & IoT Hardware Concepts", 5f);

        yield return ShowSubtitle("Area 2: Arduino Uno & IoT Hardware Concepts\r\n - Now we zoom into how you will build real IoT systems using hardware.", 7f);

        yield return ShowSubtitle("", .3f);

        yield return ShowSubtitle("How Arduino Connects to IoT?", 3.5f);

        yield return ShowSubtitle("How Arduino Connects to IoT?\r\n - Sends data to a computer, mobile app, or cloud.", 5f);

        yield return ShowSubtitle("How Arduino Connects to IoT?\r\n - Sends data to a computer, mobile app, or cloud.\r\n - Can be combined with Wi-Fi modules.", 3f);

        yield return ShowSubtitle("How Arduino Connects to IoT?\r\n - Sends data to a computer, mobile app, or cloud.\r\n - Can be combined with Wi-Fi modules.\r\n - Reads real-world signals from sensors and acts accordingly.", 6.5f);

        yield return ShowSubtitle("", .5f);

        cursor.SetActive(false);
        ArduinoCamera.SetActive(true);
        spinningArduino.SetActive(true);

        yield return ShowSubtitle("What is Arduino Uno?", 2f);

        yield return ShowSubtitle("What is Arduino Uno?\r\n - A microcontroller board that allows coding to interact with hardware.", 6f);

        yield return ShowSubtitle("What is Arduino Uno?\r\n - A microcontroller board that allows coding to interact with hardware.\r\n - Simple and widely used in IoT prototyping.", 4.8f);

        yield return ShowSubtitle("", .5f);

        spinningArduino.SetActive(false);
        ArduinoCamera.SetActive(false);

        BoardCamera.SetActive(true);
        spinningBoard.SetActive(true);

        yield return ShowSubtitle("What is Breadboard?", 2f);

        yield return ShowSubtitle("What is Breadboard?\r\n - Used to build circuits without soldering.", 5f);

        yield return ShowSubtitle("What is Breadboard?\r\n - Used to build circuits without soldering.\r\n - Connects components together by inserting wires and pins into its holes.", 6f);

        yield return ShowSubtitle("What is Breadboard?\r\n - Used to build circuits without soldering.\r\n - Connects components together by inserting wires and pins into its holes.\r\n - Great for rapid prototyping and testing circuits.", 5f);

        yield return ShowSubtitle("", .5f);

        spinningBoard.SetActive(false);
        BoardCamera?.SetActive(false);

        SensorCamera?.SetActive(true);
        spinningSensor.SetActive(true);

        yield return ShowSubtitle("What is DHT11 Sensor?", 4f);

        yield return ShowSubtitle("What is DHT11 Sensor?\r\n - Measures temperature and humidity.", 4f);

        yield return ShowSubtitle("What is DHT11 Sensor?\r\n - Measures temperature and humidity.\r\n - Sends digital signals to the Arduino.", 4f);

        yield return ShowSubtitle("What is DHT11 Sensor?\r\n - Measures temperature and humidity.\r\n - Sends digital signals to the Arduino.\r\n - Useful in smart weather stations or climate monitoring.", 5f);

        yield return ShowSubtitle("", .5f);

        spinningSensor.SetActive(false);
        SensorCamera.SetActive(false);

        WireCamera.SetActive(true);
        spinningWires.SetActive(true);
        spinningWires2.SetActive(true);

        yield return ShowSubtitle("What is Jumper Wires?", 3f);

        yield return ShowSubtitle("What is Jumper Wires?\r\n - Wires used to connect components on a breadboard or between breadboard and Arduino.", 7.5f);

        yield return ShowSubtitle("What is Jumper Wires?\r\n - Wires used to connect components on a breadboard or between breadboard and Arduino.\r\n - Types: Male-to-Male, Male-to-Female, Female-to-Female.", 5.5f);

        yield return ShowSubtitle("What is Jumper Wires?\r\n - Wires used to connect components on a breadboard or between breadboard and Arduino.\r\n - Types: Male-to-Male, Male-to-Female, Female-to-Female.\r\n - Essential for all circuit building.", 4f);

        yield return ShowSubtitle("", .5f);

        spinningWires2.SetActive(false);
        spinningWires.SetActive(false);
        WireCamera.SetActive(false);

        LEDCamera.SetActive(true);
        spinningLED.SetActive(true);

        yield return ShowSubtitle("What is LED Lights?", 2.5f);

        yield return ShowSubtitle("What is LED Lights?\r\n - Emit light when powered.", 2.8f);

        yield return ShowSubtitle("What is LED Lights?\r\n - Emit light when powered.\r\n - Often used to visualize status or outputs (on/off signals).", 3.8f);

        yield return ShowSubtitle("What is LED Lights?\r\n - Emit light when powered.\r\n - Often used to visualize status or outputs (on/off signals).\r\n - Needs a resistor to prevent burning out.", 4f);

        yield return ShowSubtitle("", .3f);

        spinningLED.SetActive(false);
        LEDCamera.SetActive(false);

        ResistorCamera?.SetActive(true);
        spinningResistor.SetActive(true);

        yield return ShowSubtitle("What is Resistor?", 2f);

        yield return ShowSubtitle("What is Resistor?\r\n - Controls the amount of current that flows through a circuit.", 5f);

        yield return ShowSubtitle("What is Resistor?\r\n - Controls the amount of current that flows through a circuit.\r\n - Protects sensitive components like LEDs.", 5f);

        yield return ShowSubtitle("What is Resistor?\r\n - Controls the amount of current that flows through a circuit.\r\n - Protects sensitive components like LEDs.\r\n - Measured in ohms (Ω).", 2.5f);

        yield return ShowSubtitle("", .5f);

        spinningResistor.SetActive(false);
        ResistorCamera.SetActive(false);

        MotorCamera.SetActive(true);    
        spinningMotor.SetActive(true);

        yield return ShowSubtitle("What is Servo Motor?", 3f);

        yield return ShowSubtitle("What is Servo Motor?\r\n - A motor that rotates to a specific angle (0° to 180°).", 7f);

        yield return ShowSubtitle("What is Servo Motor?\r\n - A motor that rotates to a specific angle (0° to 180°).\r\n - Controlled by PWM (pulse width modulation) signals from the Arduino.", 7f);

        yield return ShowSubtitle("What is Servo Motor?\r\n - A motor that rotates to a specific angle (0° to 180°).\r\n - Controlled by PWM (pulse width modulation) signals from the Arduino.\r\n - Used in smart doors, robotic arms, or automated systems.", 5.5f);

        yield return ShowSubtitle("", .5f);

        MotorCamera.SetActive(false);
        spinningMotor.SetActive(false);

        cursor.SetActive(true);
        screenboardBG.SetActive(false);
    }

    private IEnumerator ShowSubtitle(string text, float duration)
    {
        whiteboardSubtitleText.text = text;
        whiteboardSubtitleText1.text = text;

        UpdateScreenboard(text);

        yield return new WaitForSeconds(duration);
    }

    private void UpdateScreenboard(string text)
    {
        if (ArduinoCamera.activeSelf || BoardCamera.activeSelf || SensorCamera.activeSelf ||
            WireCamera.activeSelf || LEDCamera.activeSelf || ResistorCamera.activeSelf ||
            MotorCamera.activeSelf)
        {
            screenboardBG.SetActive(true);
            screenboardText.gameObject.SetActive(true);
            screenboardText.text = text;
        }
        else
        {
            screenboardBG.SetActive(false);
            screenboardText.gameObject.SetActive(false);
        }
    }

}