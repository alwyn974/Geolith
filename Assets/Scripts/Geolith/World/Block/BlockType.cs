namespace Geolith.World.Block
{
    public enum BlockType // TODO: replace this with a class like Minecraft Blocks.BLOCK_TYPE with a registry
    {
        Nothing = -1, // If limited map with chunks not loaded/rendered
        Air = 0,
        Stone,
        Dirt,
        Grass,
        Log,
        Leaves,
        Planks,
        Glass,
        Sand,
        Water,
        Lava,
        Bedrock, // find another name for this
    }
}