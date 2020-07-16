using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Extensions
{
    public static class CharacterExtensions
    {
        public static float getPistolSkillModifier(this ICharacter<float> character)
        {
            float skill = character.PistolSkill;
            return skill / (1250 + skill);
        }

        public static float getRifleSkillModifier(this ICharacter<float> character)
        {
            float skill = character.RifleSkill;
            return skill / (1250 + skill);
        }

        public static float getHeavyRifleSkillModifier(this ICharacter<float> character)
        {
            float skill = character.HeavyRifleSkill;
            return skill / (1250 + skill);
        }

        public static float getShotgunSkillModifier(this ICharacter<float> character)
        {
            float skill = character.ShotgunSkill;
            return skill / (1250 + skill);
        }
    }
}