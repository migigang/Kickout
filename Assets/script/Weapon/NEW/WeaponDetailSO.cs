using UnityEngine;

[CreateAssetMenu(fileName ="WeaponDetail_", menuName ="Data/Weapon")]

public class WeaponDetailSO : ScriptableObject
{
    #region Header WEAPON BASE DETAILS
    [Space(10)]
    [Header("WEAPON BASE DETAILS")]
    #endregion Header WEAPON BASE DETAILS
    #region Tooltip
    [Tooltip("Weapon Name")]
    #endregion Tooltip
    public string weaponName;

    #region Tooltip
    [Tooltip("The sprite for the weapon - the sprite should have the 'generate physics shape' option selected")]
    #endregion Tooltip    
    public Sprite weaponSprite;

    #region Header WEAPON CONFIGURATION
    [Space(10)]
    [Header("WEAPON CONFIGURATION")]    
    #endregion Header WEAPON CONFIGURATION
    public Vector3 weaponShootPosition;

    //#region Tooltip
    //[Tooltip("Weapon current ammo")]  
    //#endregion tooltip
    //public AmmoDetailsSO weaponCurrentAmmo;

    #region Header WEAPON OPERATING VALUES
    [Space(10)]
    [Header("WEAPON OPERATING VALUES")]
    #endregion Header WEAPON OPERATING VALUES
    #region Tooltip
    [Tooltip("Select if the weapon has infinite ammo")]
    #endregion Tooltip
    public bool hasInfiniteAmmo = false;

    #region Tooltip
    [Tooltip("Select if the weapon has infinite clip capacity")]
    #endregion Tooltip
    public bool hasInfiniteClipCapacity = false;

    #region Tooltip
    [Tooltip("the weapon capacity - shots before a reload")]
    #endregion Tooltip
    public int weaponClipAmmoCapacity = 6;

    #region Tooltip
    [Tooltip("Weapon ammo capacity - the maximum number of rounds at that can be held for this weapon")]
    #endregion Tooltip
    public int weaponAmmoCapacity = 100;

    #region Tooltip
    [Tooltip("Weapon fire rate - 0.2 means 5 shots a second")]
    #endregion Tooltip
    public float weaponFireRate = 0.2f;

    #region Tooltip
    [Tooltip("Weapon precharge time - the maximum number to hold fire button down before firing")]
    #endregion Tooltip
    public float weaponPrechargeTime = 0f;
    
    #region Tooltip
    [Tooltip("this is the weapon reload time in seconds")]
    #endregion Tooltip
    public float weaponReloadTime = 0f;


    #region Validation
#if UNITY_EDITOR

    
    private void OnValidate()
    {
       // HelperUtilities.ValidateCheckEmptyString(this, nameof(weaponName), weaponName);
        //HelperUtilities.ValidateCheckNullValue(this, nameof(weaponCurrentAmmo), weaponCurrentAmmo);
       // HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponFireRate), weaponFireRate, false);
       // HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponPrechargeTime), weaponPrechargeTime, true);
        
       // if(!hasInfiniteAmmo){
        //    HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponAmmoCapacity), weaponAmmoCapacity, false);
        // }
        //if(!hasInfiniteClipCapacity){
        //    HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponClipAmmoCapacity), weaponClipAmmoCapacity, false);
        //}
        
    }
#endif
    #endregion Validation


}