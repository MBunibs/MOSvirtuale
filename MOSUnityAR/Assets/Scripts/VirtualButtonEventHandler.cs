/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This class implements the IVirtualButtonEventHandler interface and
/// contains the logic to setActive the modules of the structure
/// depending on what virtual button has been pressed.
/// </summary>
public class VirtualButtonEventHandler : MonoBehaviour,
                                         IVirtualButtonEventHandler
{
    #region PUBLIC_MEMBER_VARIABLES

	//dichiaro pubbliche le variabili che contengono i due materiali per i bottoni di sezioni premuti o meno
	public Material ButtonPressedIllumMaterial;
	public Material ButtonPressedDiffMaterial;

	//valore traslazione bottoni cubo
	public float tranValue = 0.03f;
	//valore traslazione bordo di selezione per ossidi
	public float tranValueBorder = 0.06f;

    #endregion // PUBLIC_MEMBER_VARIABLES



    #region PRIVATE_MEMBER_VARIABLES
    
	//dichiaro le variabili per i moduli della struttura
    private GameObject mTransistor11;
	private GameObject mTransistor12;
	private GameObject mTransistor13;
	private GameObject mTransistor14;

	private GameObject mTransistor21;
	private GameObject mTransistor22;
	private GameObject mTransistor23;
	private GameObject mTransistor24;

	private GameObject mTransistor31;
	private GameObject mTransistor32;
	private GameObject mTransistor33;
	private GameObject mTransistor34;

	private GameObject mTransistor41;
	private GameObject mTransistor42;
	private GameObject mTransistor43;
	private GameObject mTransistor44;

	//dichiaro le variabili per le versioni con e senza ossidi
	private GameObject mTransistorNoOxide;
	private GameObject mTransistorCompleto;

	//dichiaro le variabili per i piani di sezione visibili alla pressione del tasto
	private GameObject mSectionPlaneRow1;
	private GameObject mSectionPlaneRow2;
	private GameObject mSectionPlaneRow3;
	private GameObject mSectionPlaneColumn1;
	private GameObject mSectionPlaneColumn2;
	private GameObject mSectionPlaneColumn3;

	//dichiaro le variabili per le linee di sezione visibili quando premuto il bottone corrispondente
	private GameObject mSectionPlaneBaseRow1;
	private GameObject mSectionPlaneBaseRow2;
	private GameObject mSectionPlaneBaseRow3;
	private GameObject mSectionPlaneBaseColumn1;
	private GameObject mSectionPlaneBaseColumn2;
	private GameObject mSectionPlaneBaseColumn3;

	//dichiaro le variabili che contengono i bottoni cubo di selezione
	private GameObject mButtonPressedRow1;
	private GameObject mButtonPressedRow2;
	private GameObject mButtonPressedRow3;
	private GameObject mButtonPressedColumn1;
	private GameObject mButtonPressedColumn2;
	private GameObject mButtonPressedColumn3;

	//dichiaro la variabili che contiene il bottone cubo per gli ossidi
	private GameObject mButtonSiO2;

	//dichiaro la variabili che contiene l'oggetto per la selezione dello stato per gli ossidi
	private GameObject mSelectionBorderSiO2;

	//dichiaro variabili per contenere la posizione originaria dei bottoni cubo 
	private Vector3 mButtonOriginRow1;
	private Vector3 mButtonOriginRow2;
	private Vector3 mButtonOriginRow3;
	private Vector3 mButtonOriginColumn1;
	private Vector3 mButtonOriginColumn2;
	private Vector3 mButtonOriginColumn3;

	//dichiaro variabile per contenere la posizione originaria del bottone cubo per ossidi 
	private Vector3 mButtonOriginSiO2;

	//dichiaro variabile per contenere la posizione originaria del bottone cubo per ossidi
	private Vector3 mSelectionBorderOriginSiO2;

	//dichiaro la variabile per gestire l'evento del  bottone degli ossidi in modalità "acceso/spento"
	private bool reset = false;
	//DA ELIMINARE?
	private DefaultTrackableEventHandler mioDefaultTEH;
    
	#endregion // PRIVATE_MEMBER_VARIABLES



    #region UNITY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        // Register with the virtual buttons TrackableBehaviour
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            vbs[i].RegisterEventHandler(this);
        }

        //Recupero i moduli oggetto dalla scena attraverso il nome
        mTransistor11 = transform.FindChild("Transistor 1,1").gameObject;
		mTransistor12 = transform.FindChild("Transistor 1,2").gameObject;
		mTransistor13 = transform.FindChild("Transistor 1,3").gameObject;
		mTransistor14 = transform.FindChild("Transistor 1,4").gameObject;

		mTransistor21 = transform.FindChild("Transistor 2,1").gameObject;
		mTransistor22 = transform.FindChild("Transistor 2,2").gameObject;
		mTransistor23 = transform.FindChild("Transistor 2,3").gameObject;
		mTransistor24 = transform.FindChild("Transistor 2,4").gameObject;

		mTransistor31 = transform.FindChild("Transistor 3,1").gameObject;
		mTransistor32 = transform.FindChild("Transistor 3,2").gameObject;
		mTransistor33 = transform.FindChild("Transistor 3,3").gameObject;
		mTransistor34 = transform.FindChild("Transistor 3,4").gameObject;

		mTransistor41 = transform.FindChild("Transistor 4,1").gameObject;
		mTransistor42 = transform.FindChild("Transistor 4,2").gameObject;
		mTransistor43 = transform.FindChild("Transistor 4,3").gameObject;
		mTransistor44 = transform.FindChild("Transistor 4,4").gameObject;

		mTransistorNoOxide = transform.FindChild("TransistorNoOxide").gameObject;
		mTransistorCompleto = transform.FindChild("TransistorCompleto").gameObject;


		//Recupero i piani di sezione dalla scena attraverso il nome
		mSectionPlaneRow1 = transform.FindChild("SectionPlaneRow1").gameObject;
		mSectionPlaneRow2 = transform.FindChild("SectionPlaneRow2").gameObject;
		mSectionPlaneRow3 = transform.FindChild("SectionPlaneRow3").gameObject;
		mSectionPlaneColumn1 = transform.FindChild("SectionPlaneColumn1").gameObject;
		mSectionPlaneColumn2 = transform.FindChild("SectionPlaneColumn2").gameObject;
		mSectionPlaneColumn3 = transform.FindChild("SectionPlaneColumn3").gameObject;

		//Recupero le linee di sezione dalla scena attraverso il nome
		mSectionPlaneBaseRow1 = transform.FindChild ("SectionPlaneRowBase1").gameObject;
		mSectionPlaneBaseRow2 = transform.FindChild ("SectionPlaneRowBase2").gameObject;
		mSectionPlaneBaseRow3 = transform.FindChild ("SectionPlaneRowBase3").gameObject;
		mSectionPlaneBaseColumn1 = transform.FindChild ("SectionPlaneColumnBase1").gameObject;
		mSectionPlaneBaseColumn2 = transform.FindChild ("SectionPlaneColumnBase2").gameObject;
		mSectionPlaneBaseColumn3 = transform.FindChild ("SectionPlaneColumnBase3").gameObject;

		//Recupero i piani che evidenziono la pressione del bottone dalla scena attraverso il nome
		mButtonPressedRow1 = transform.FindChild("ButtonPressedRow1").gameObject;
		mButtonPressedRow2 = transform.FindChild("ButtonPressedRow2").gameObject;
		mButtonPressedRow3 = transform.FindChild("ButtonPressedRow3").gameObject;
		mButtonPressedColumn1 = transform.FindChild("ButtonPressedColumn1").gameObject;
		mButtonPressedColumn2 = transform.FindChild("ButtonPressedColumn2").gameObject;
		mButtonPressedColumn3 = transform.FindChild("ButtonPressedColumn3").gameObject;

		//Recupero il bottone per gli ossidi attraverso il nome
		mButtonSiO2 = transform.FindChild ("ButtonSiO2").gameObject;

		//Recupero l'oggetto per la selezione degli ossidi attraverso il nome
		mSelectionBorderSiO2 = transform.FindChild ("SelectionBorderSiO2").gameObject;

		//imposto a non attivi (quindi non presenti nella scena) tutti gli oggetti che non servono all'avvio
		mTransistor11.SetActive(false);
		mTransistor12.SetActive(false);
		mTransistor13.SetActive(false);
		mTransistor14.SetActive(false);
		mTransistor21.SetActive(false);
		mTransistor22.SetActive(false);
		mTransistor23.SetActive(false);
		mTransistor24.SetActive(false);
		mTransistor31.SetActive(false);
		mTransistor32.SetActive(false);
		mTransistor33.SetActive(false);
		mTransistor34.SetActive(false);
		mTransistor41.SetActive(false);
		mTransistor42.SetActive(false);
		mTransistor43.SetActive(false);
		mTransistor44.SetActive(false);

		mSectionPlaneRow1.SetActive(false);
		mSectionPlaneRow2.SetActive(false);
		mSectionPlaneRow3.SetActive(false);
		mSectionPlaneColumn1.SetActive(false);
		mSectionPlaneColumn2.SetActive(false);
		mSectionPlaneColumn3.SetActive(false);

		mSectionPlaneBaseRow1.SetActive(false);
		mSectionPlaneBaseRow2.SetActive(false);
		mSectionPlaneBaseRow3.SetActive(false);
		mSectionPlaneBaseColumn1.SetActive(false);
		mSectionPlaneBaseColumn2.SetActive(false);
		mSectionPlaneBaseColumn3.SetActive(false);

		mTransistorNoOxide.SetActive(false);

		//all'avvio rendo visibile solo il transistor completo
		mTransistorCompleto.SetActive(true);

		//salvo le posizioni originali dei bottoni
		mButtonOriginRow1 = mButtonPressedRow1.transform.localPosition;
		mButtonOriginRow2 = mButtonPressedRow2.transform.localPosition;
		mButtonOriginRow3 = mButtonPressedRow3.transform.localPosition;
		mButtonOriginColumn1 = mButtonPressedColumn1.transform.localPosition;
		mButtonOriginColumn2 = mButtonPressedColumn2.transform.localPosition;
		mButtonOriginColumn3 = mButtonPressedColumn3.transform.localPosition;

		//salvo la posizione originaria del bottone per gli ossidi
		mButtonOriginSiO2 = mButtonSiO2.transform.localPosition;

		//salvo la posizione originaria dell'oggetto per la selezione degli ossidi
		mSelectionBorderOriginSiO2 = mSelectionBorderSiO2.transform.localPosition;
   	}

	#endregion // UNITY_MONOBEHAVIOUR_METHODS





    #region PUBLIC_METHODS
    
	/// <summary>
    /// Called when the virtual button has just been pressed:
	/// </summary>


	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
	{
		Debug.Log("OnButtonPressed::" + vb.VirtualButtonName);

		//alla pressione di qualunque bottone mostro tutti i moduli della struttura
		mTransistor11.SetActive(true);
		mTransistor12.SetActive(true);
		mTransistor13.SetActive(true);
		mTransistor14.SetActive(true);
		mTransistor21.SetActive(true);
		mTransistor22.SetActive(true);
		mTransistor23.SetActive(true);
		mTransistor24.SetActive(true);
		mTransistor31.SetActive(true);
		mTransistor32.SetActive(true);
		mTransistor33.SetActive(true);
		mTransistor34.SetActive(true);
		mTransistor41.SetActive(true);
		mTransistor42.SetActive(true);
		mTransistor43.SetActive(true);
		mTransistor44.SetActive(true);

		//alla pressione di qualunque bottone nascondo il modello senza ossidi e quello completo
		mTransistorNoOxide.SetActive(false);
		mTransistorCompleto.SetActive(false);

		//alla pressione di qualunque bottone nascondo tutti i piani di sezione
		mSectionPlaneBaseRow1.SetActive(false);
		mSectionPlaneBaseRow2.SetActive(false);
		mSectionPlaneBaseRow3.SetActive(false);
		mSectionPlaneBaseColumn1.SetActive(false);
		mSectionPlaneBaseColumn2.SetActive(false);
		mSectionPlaneBaseColumn3.SetActive(false);

		//alla pressione di qualunque bottone reimposto all'origine tutti i bottoni
		mButtonPressedRow1.transform.localPosition = mButtonOriginRow1;
		mButtonPressedRow2.transform.localPosition = mButtonOriginRow2;
		mButtonPressedRow3.transform.localPosition = mButtonOriginRow3;
		mButtonPressedColumn1.transform.localPosition = mButtonOriginColumn1;
		mButtonPressedColumn2.transform.localPosition = mButtonOriginColumn2;
		mButtonPressedColumn3.transform.localPosition = mButtonOriginColumn3;

		//alla pressione di qualunque bottone reimposto i materiali dei bottoni di sezione al materiale di default
		mButtonPressedRow1.transform.renderer.material = ButtonPressedDiffMaterial;
		mButtonPressedRow2.transform.renderer.material = ButtonPressedDiffMaterial;
		mButtonPressedRow3.transform.renderer.material = ButtonPressedDiffMaterial;
		mButtonPressedColumn1.transform.renderer.material = ButtonPressedDiffMaterial;
		mButtonPressedColumn2.transform.renderer.material = ButtonPressedDiffMaterial;
		mButtonPressedColumn3.transform.renderer.material = ButtonPressedDiffMaterial;

		switch (vb.VirtualButtonName) {
		case "row 1":
			//alla pressione del tasto:
			//rendo attivo il piano di sezione corrispondente
			mSectionPlaneRow1.SetActive(true);
			//rendo attiva la linea di sezione sulla base
			mSectionPlaneBaseRow1.SetActive(true);
			//traslo il bottone cubo verso il basso del valore tranValue
			mButtonPressedRow1.transform.localPosition = new Vector3(mButtonPressedRow1.transform.localPosition.x, mButtonPressedRow1.transform.localPosition.y - tranValue, mButtonPressedRow1.transform.localPosition.z);
			//modifico il materiale del bottone
			mButtonPressedRow1.transform.renderer.material = ButtonPressedIllumMaterial;
			break;

		case "row 2":
			mSectionPlaneRow2.SetActive(true);
			mSectionPlaneBaseRow2.SetActive(true);
			mButtonPressedRow2.transform.localPosition = new Vector3(mButtonPressedRow2.transform.localPosition.x, mButtonPressedRow2.transform.localPosition.y - tranValue, mButtonPressedRow2.transform.localPosition.z);
			mButtonPressedRow2.transform.renderer.material = ButtonPressedIllumMaterial;
			break;
		
		case "row 3":
			mSectionPlaneRow3.SetActive(true);
			mSectionPlaneBaseRow3.SetActive(true);
			mButtonPressedRow3.transform.localPosition = new Vector3(mButtonPressedRow3.transform.localPosition.x, mButtonPressedRow3.transform.localPosition.y - tranValue, mButtonPressedRow3.transform.localPosition.z);
			mButtonPressedRow3.transform.renderer.material = ButtonPressedIllumMaterial;
			break;
			
		case "column 1":
			mSectionPlaneColumn1.SetActive(true);
			mSectionPlaneBaseColumn1.SetActive(true);
			mButtonPressedColumn1.transform.localPosition = new Vector3(mButtonPressedColumn1.transform.localPosition.x, mButtonPressedColumn1.transform.localPosition.y - tranValue, mButtonPressedColumn1.transform.localPosition.z);
			mButtonPressedColumn1.transform.renderer.material = ButtonPressedIllumMaterial;
			break;

		case "column 2":
			mSectionPlaneColumn2.SetActive(true);
			mSectionPlaneBaseColumn2.SetActive(true);
			mButtonPressedColumn2.transform.localPosition = new Vector3(mButtonPressedColumn2.transform.localPosition.x, mButtonPressedColumn2.transform.localPosition.y - tranValue, mButtonPressedColumn2.transform.localPosition.z);
			mButtonPressedColumn2.transform.renderer.material = ButtonPressedIllumMaterial;
			break;
			
		case "column 3":
			mSectionPlaneColumn3.SetActive(true);
			mSectionPlaneBaseColumn3.SetActive(true);
			mButtonPressedColumn3.transform.localPosition = new Vector3(mButtonPressedColumn3.transform.localPosition.x, mButtonPressedColumn3.transform.localPosition.y - tranValue, mButtonPressedColumn3.transform.localPosition.z);
			mButtonPressedColumn3.transform.renderer.material = ButtonPressedIllumMaterial;
			break;

		case "noOxide":
			//alla pressione del tasto traslo verso il basso il bottone degli ossidi di tranValue 
			mButtonSiO2.transform.localPosition = new Vector3(mButtonSiO2.transform.localPosition.x, mButtonSiO2.transform.localPosition.y - tranValue, mButtonSiO2.transform.localPosition.z);

			if(reset == false){
				//rendo visibile il modello senza ossidi
				mTransistorNoOxide.SetActive(true);

				//rendo nascosti tutti i moduli
				mTransistor11.SetActive(false);
				mTransistor12.SetActive(false);
				mTransistor13.SetActive(false);
				mTransistor14.SetActive(false);
				mTransistor21.SetActive(false);
				mTransistor22.SetActive(false);
				mTransistor23.SetActive(false);
				mTransistor24.SetActive(false);
				mTransistor31.SetActive(false);
				mTransistor32.SetActive(false);
				mTransistor33.SetActive(false);
				mTransistor34.SetActive(false);
				mTransistor41.SetActive(false);
				mTransistor42.SetActive(false);
				mTransistor43.SetActive(false);
				mTransistor44.SetActive(false);

				//traslo l'oggetto selettore dello stato degli ossidi verso il basso di tranValueBorder
				mSelectionBorderSiO2.transform.localPosition = new Vector3(mSelectionBorderSiO2.transform.localPosition.x, mSelectionBorderSiO2.transform.localPosition.y, mSelectionBorderSiO2.transform.localPosition.z - tranValueBorder);

				//imposto la variabile reset a true così da sapere che sono nello stato con ossidi
				reset = true;
			}
			else{
				//rendo invisibile il modello senza ossidi
				mTransistorNoOxide.SetActive(false);

				//rendo visibili tutti i moduli
				mTransistor11.SetActive(true);
				mTransistor12.SetActive(true);
				mTransistor13.SetActive(true);
				mTransistor14.SetActive(true);
				mTransistor21.SetActive(true);
				mTransistor22.SetActive(true);
				mTransistor23.SetActive(true);
				mTransistor24.SetActive(true);
				mTransistor31.SetActive(true);
				mTransistor32.SetActive(true);
				mTransistor33.SetActive(true);
				mTransistor34.SetActive(true);
				mTransistor41.SetActive(true);
				mTransistor42.SetActive(true);
				mTransistor43.SetActive(true);
				mTransistor44.SetActive(true);

				//traslo l'oggetto selettore dello stato degli ossidi verso l'alto, ovvero nella posizione originale
				mSelectionBorderSiO2.transform.localPosition = mSelectionBorderOriginSiO2;

				//imposto la variabile reset a false così da sapere che sono uscito dallo stato con ossidi
				reset = false;
			}
		break;
		}


	}


    /// <summary>
    /// Called when the virtual button has just been released:
    /// </summary>
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
		Debug.Log("OnButtonReleased::" + vb.VirtualButtonName);
		switch (vb.VirtualButtonName)
        {
		case "row 1":
			//al rilascio del bottone:
			//attivo i moduli inerenti alla sezione associata al bottone
			mTransistor11.SetActive(true);
			mTransistor12.SetActive(true);
			mTransistor13.SetActive(true);
			mTransistor14.SetActive(true);
			//disattivo i moduli non inerenti alla sezione associata al bottone
			mTransistor21.SetActive(false);
			mTransistor22.SetActive(false);
			mTransistor23.SetActive(false);
			mTransistor24.SetActive(false);
			mTransistor31.SetActive(false);
			mTransistor32.SetActive(false);
			mTransistor33.SetActive(false);
			mTransistor34.SetActive(false);
			mTransistor41.SetActive(false);
			mTransistor42.SetActive(false);
			mTransistor43.SetActive(false);
			mTransistor44.SetActive(false);

			//rendo invisibile il piano di sezione
			mSectionPlaneRow1.SetActive(false);
            break;
		
		case "row 2":
			mTransistor11.SetActive(true);
			mTransistor12.SetActive(true);
			mTransistor13.SetActive(true);
			mTransistor14.SetActive(true);
			mTransistor21.SetActive(true);
			mTransistor22.SetActive(true);
			mTransistor23.SetActive(true);
			mTransistor24.SetActive(true);
			mTransistor31.SetActive(false);
			mTransistor32.SetActive(false);
			mTransistor33.SetActive(false);
			mTransistor34.SetActive(false);
			mTransistor41.SetActive(false);
			mTransistor42.SetActive(false);
			mTransistor43.SetActive(false);
			mTransistor44.SetActive(false);

			mSectionPlaneRow2.SetActive(false);	
			break;

		case "row 3":
			mTransistor11.SetActive(true);
			mTransistor12.SetActive(true);
			mTransistor13.SetActive(true);
			mTransistor14.SetActive(true);
			mTransistor21.SetActive(true);
			mTransistor22.SetActive(true);
			mTransistor23.SetActive(true);
			mTransistor24.SetActive(true);
			mTransistor31.SetActive(true);
			mTransistor32.SetActive(true);
			mTransistor33.SetActive(true);
			mTransistor34.SetActive(true);
			mTransistor41.SetActive(false);
			mTransistor42.SetActive(false);
			mTransistor43.SetActive(false);
			mTransistor44.SetActive(false);
			
			mSectionPlaneRow3.SetActive(false);
			break;

		case "column 1":
			mTransistor11.SetActive(true);
			mTransistor21.SetActive(true);
			mTransistor31.SetActive(true);
			mTransistor41.SetActive(true);
			mTransistor12.SetActive(false);
			mTransistor22.SetActive(false);
			mTransistor32.SetActive(false);
			mTransistor42.SetActive(false);
			mTransistor13.SetActive(false);
			mTransistor23.SetActive(false);
			mTransistor33.SetActive(false);
			mTransistor43.SetActive(false);
			mTransistor14.SetActive(false);
			mTransistor24.SetActive(false);
			mTransistor34.SetActive(false);
			mTransistor44.SetActive(false);

			mSectionPlaneColumn1.SetActive(false);
			break;

		case "column 2":
			mTransistor11.SetActive(true);
			mTransistor21.SetActive(true);
			mTransistor31.SetActive(true);
			mTransistor41.SetActive(true);
			mTransistor12.SetActive(true);
			mTransistor22.SetActive(true);
			mTransistor32.SetActive(true);
			mTransistor42.SetActive(true);
			mTransistor13.SetActive(false);
			mTransistor23.SetActive(false);
			mTransistor33.SetActive(false);
			mTransistor43.SetActive(false);
			mTransistor14.SetActive(false);
			mTransistor24.SetActive(false);
			mTransistor34.SetActive(false);
			mTransistor44.SetActive(false);
			
			mSectionPlaneColumn2.SetActive(false);
			break;

		case "column 3":
			mTransistor11.SetActive(true);
			mTransistor21.SetActive(true);
			mTransistor31.SetActive(true);
			mTransistor41.SetActive(true);
			mTransistor12.SetActive(true);
			mTransistor22.SetActive(true);
			mTransistor32.SetActive(true);
			mTransistor42.SetActive(true);
			mTransistor13.SetActive(true);
			mTransistor23.SetActive(true);
			mTransistor33.SetActive(true);
			mTransistor43.SetActive(true);
			mTransistor14.SetActive(false);
			mTransistor24.SetActive(false);
			mTransistor34.SetActive(false);
			mTransistor44.SetActive(false);

			mSectionPlaneColumn3.SetActive(false);
			break;

		case "noOxide":
			//reimposto la posizione dell'oggetto di selezione dello stato degli ossidi
			mButtonSiO2.transform.localPosition = mButtonOriginSiO2;
			break;
        }
    }

    #endregion // PUBLIC_METHODS
}
