using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReader : MonoBehaviour
{

    public static FileReader instance;
    private TextAsset resource;
    private string textoDelResource = "";
    private string[] lineas;
    private string[] palabras;

    private void Awake()
    {
        if (instance != this) {
            DestroyImmediate(instance);
        }
        instance = this;
    }

    public List<int> GetNewBinaryLevel(){
        List<int> positions = new List<int>();
        int[] leftSide = new int[10];
        int[] rightSide = new int[10];
        int[] finalArray = new int[leftSide.Length + rightSide.Length];

        if (Random.Range(0f, 1f) < 0.5f)
        {
            leftSide = GenerateBinarySide();;
            rightSide = GenerateOtherSide(leftSide);
        }
        else{
            rightSide = GenerateBinarySide();;
            leftSide = GenerateOtherSide(rightSide);
        }

        leftSide.CopyTo(finalArray, 0);
        rightSide.CopyTo(finalArray, leftSide.Length);

        for (int j = 0; j < finalArray.Length; j++){
            positions.Add(finalArray[j]);
        }
    return positions;
    }

    public List<int> GetNewLevel(int diff){
        List<int> positions = new List<int>();
        int[] leftSide = new int[10];
        int[] rightSide = new int[10];
        int[] finalArray = new int[leftSide.Length + rightSide.Length];

        if (Random.Range(0f, 1f) < 0.5f)
        {
            int[] thisArray = GenerateSide(diff+1);
            for(int i=0; i<leftSide.Length; i++){
                leftSide[i] = thisArray[i];
                //Debug.Log("["+thisArray[i]+"]");
            }
            rightSide = GenerateOtherSide(leftSide);
        }
        else{
            rightSide = GenerateSide(diff+1);
            leftSide = GenerateOtherSide(rightSide);
        }

        leftSide.CopyTo(finalArray, 0);
        rightSide.CopyTo(finalArray, leftSide.Length);

        for (int j = 0; j < finalArray.Length; j++){
            positions.Add(finalArray[j]);
        }
    return positions;
    }

    public List<int> ReadTheFile(int file)
    {
        string path = "Blocks/level" + file.ToString();
        List<int> positions = new List<int>();
        resource = (TextAsset) Resources.Load(path);
        textoDelResource = resource.text;

        lineas = textoDelResource.Split('\n');
        char delimitador = '-';
        int alea = Random.Range(0, 2);
        if (alea >= 1)
        {
            for (int i = 0; i < lineas.Length; i++)
            {
                palabras = lineas[i].Split(delimitador);
                for (int j = 0; j < palabras.Length; j++)
                {
                    positions.Add(int.Parse(palabras[j]));
                }
            }
        }
        else {
            for (int i = (lineas.Length-1); i > -1; i--)
            {
                palabras = lineas[i].Split(delimitador);
                for (int j = 0; j < palabras.Length; j++)
                {
                    positions.Add(int.Parse(palabras[j]));
                }
            }
        }

        return positions;
    }


//-------------------------------------------------------------------------

    public int[] GenerateSide(int difficult){
        int[] side = new int[10];

        bool value = false;

        int auxDiff = 0;
        int thisDifficult = side.Length/difficult;

        value = GetRandomVal(0.5f);

        for (int i = 0; i < side.Length; i++)
        {
            auxDiff++;
            if (auxDiff == thisDifficult){
                auxDiff = 0;
                if (difficult == 5){
                    if(GetRandomVal(0.5f))
                        value = GetRandomVal(0.3f);
                }else{
                    value = !value;
                }


            }
            if (value)
                side[i] = 1;
            else
                side[i] = 0;
        }

        return side;

    }

    public int[] GenerateOtherSide(int[] otherSide){
        int[] thisSide = new int[10];

        for (int i = 0; i < otherSide.Length; i++)
        {
            if( otherSide[i] == 0 )
                thisSide[i] = 1;
            else
                thisSide[i] = 0;                
        }

        return thisSide;
    }

    public int[] GenerateBinarySide(){
        char[] binaryChar = new char[10];
        int[] binaryFinal = new int[10];

        int decNumber = 0;
        string binaryNumber = "";

        int difference = 0;

        decNumber = Random.Range(0,1023);

        binaryNumber = System.Convert.ToString(decNumber, 2);

        //Debug.Log("Numero : " + binaryNumber);
        //Debug.Log("Numero tamaño : " + binaryNumber.Length);

        binaryChar = binaryNumber.ToCharArray();

        for (int i=0; i<binaryChar.Length; i++){
           binaryFinal[i] = System.Convert.ToInt32(new string(binaryChar[i], 1));
        }

        if(binaryFinal.Length < 10){
            difference = 10 - binaryFinal.Length;
            int j = binaryFinal.Length+1;
            while(difference > 0){
                binaryFinal[j] = Random.Range(0,2);
                j++;
                difference--;
            }
        }
        
        CheckBinaryNumber(binaryFinal);

        return binaryFinal;
    }

    public void CheckBinaryNumber(int[] binarySide){
        int thisNumber = 0;
        bool sameNumber = true;

        for(int i = 0; i < binarySide.Length; i++){
            if (i == 0){
                thisNumber = binarySide[i];
            }else{
                if (binarySide[i] != thisNumber){
                    sameNumber = false;
                }
            }
        }
        if (sameNumber){
            binarySide = GenerateBinarySide();
        }
    }

    public bool GetRandomVal(float val){
        return (Random.Range(0f, 1f) < val);
    }

}
