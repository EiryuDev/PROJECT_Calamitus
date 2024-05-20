using Nutbusterz.Calamitus;
using UnityEngine;

public class AICharacterCombatManager : CharacterCombatManager
{
    [HideInInspector] public AICharacterManager aiCharacter;

    [Header("ATTACK DATA")]
    public Transform firePoint; // Point from where the bullet will be fired
    private float nextFireTime = 0f;
    protected override void Awake()
    {
        base.Awake();
        aiCharacter = GetComponent<AICharacterManager>();
    }

    public void AttemptToAttack(Transform target)
    {
        if (Time.time >= nextFireTime)
        {
            AttemptToChooseAttack(target);
            nextFireTime = Time.time + 1f / aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.aiAttackTime;
        }
    }
    private void AttemptToChooseAttack(Transform target)
    {
        switch (aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.aiTypes)
        {
            case AITypes.Floating:
                AttemptToShootBullets(target);
                break;
            case AITypes.Grounded:
                AttemptToHitPlayer(target);
                break;
        }
    }
    private void AttemptToShootBullets(Transform target)
    {
        // Calculate the direction from the enemy to the player in 3D space
        Vector3 direction3D = (target.position - firePoint.position).normalized;

        // Convert the 3D direction to a 2D direction by ignoring the y-component
        Vector3 direction2D = new Vector3(direction3D.x, direction3D.y, direction3D.z);

        // Spawn the bullet
        GameObject bullet = Instantiate(aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.weaponModel, firePoint.position, Quaternion.identity);

        // Apply the calculated direction and force to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        WRLD_PROJECTILE_MANAGER projectile = bullet.GetComponent<WRLD_PROJECTILE_MANAGER>();
        rb.linearVelocity = direction2D * projectile.currentProjectileItemBeingUsed.projectileSpeed;
    }

    private void AttemptToHitPlayer(Transform target)
    {
        Debug.Log("Attacking Player");
        PlayerManager player = FindFirstObjectByType<PlayerManager>();
        player.playerStatsManager.TakeDamage(aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.aiDamage);
    }
}
