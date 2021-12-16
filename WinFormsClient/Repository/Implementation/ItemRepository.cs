using Common.Entity;
using WinFormsClient.Model;
using WinFormsClient.Repository.Interface;

namespace WinFormsClient.Repository.Implementation;

internal class ItemRepository : IItemRepository
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IDictionary<int, VisualAnimation> _animations = new Dictionary<int, VisualAnimation>();
    private readonly IDictionary<int, VisualCheckersSkin> _skins = new Dictionary<int, VisualCheckersSkin>();
    private readonly IDictionary<int, VisualLootBox> _lootBoxes = new Dictionary<int, VisualLootBox>();
    private readonly IDictionary<int, VisualAchievement> _achievements = new Dictionary<int, VisualAchievement>();

    public ItemRepository(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    private Task<Image> GetImage(Item i) => _resourceRepository.Get(i);

    public async Task<VisualAnimation> Get(Animation animation)
    {
        if(_animations.ContainsKey(animation.Id))
            return _animations[animation.Id];
        _animations.Add(animation.Id, new VisualAnimation(animation, await GetImage(animation)));
        return _animations[animation.Id];
    }

    public async Task<IEnumerable<VisualAnimation>> Get(IEnumerable<Animation> animations)
    {
        var result = new List<VisualAnimation>();
        foreach(var a in animations)
            result.Add(await Get(a));
        return result;
    }

    public async Task<VisualCheckersSkin> Get(CheckersSkin skin)
    {
        if (_skins.ContainsKey(skin.Id))
            return _skins[skin.Id];
        _skins.Add(skin.Id, new VisualCheckersSkin(skin, await GetImage(skin)));
        return _skins[skin.Id];
    }

    public async Task<IEnumerable<VisualCheckersSkin>> Get(IEnumerable<CheckersSkin> skins)
    {
        var result = new List<VisualCheckersSkin>();
        foreach (var a in skins)
            result.Add(await Get(a));
        return result;
    }

    public async Task<VisualLootBox> Get(LootBox lootBox)
    {
        if (_lootBoxes.ContainsKey(lootBox.Id))
            return _lootBoxes[lootBox.Id];
        _lootBoxes.Add(lootBox.Id, new VisualLootBox(lootBox, await GetImage(lootBox)));
        return _lootBoxes[lootBox.Id];
    }

    public async Task<IEnumerable<VisualLootBox>> Get(IEnumerable<LootBox> lootBoxes)
    {
        var result = new List<VisualLootBox>();
        foreach (var a in lootBoxes)
            result.Add(await Get(a));
        return result;
    }

    public async Task<VisualAchievement> Get(Achievement achievement)
    {
        if (_achievements.ContainsKey(achievement.Id))
            return _achievements[achievement.Id];
        _achievements.Add(achievement.Id, new VisualAchievement(achievement, await GetImage(achievement)));
        return _achievements[achievement.Id];
    }

    public async Task<IEnumerable<VisualAchievement>> Get(IEnumerable<Achievement> achievements)
    {
        var result = new List<VisualAchievement>();
        foreach (var a in achievements)
            result.Add(await Get(a));
        return result;
    }
}