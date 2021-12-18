using Api.Interface;
using Common.Entity;
using WinFormsClient.Model;
using WinFormsClient.Model.Item;
using WinFormsClient.Repository.Interface;

namespace WinFormsClient.Repository.Implementation;

internal class UserRepository : IUserRepository
{
    private readonly IAsyncUserApi _userApi;
    private readonly IItemRepository _itemRepository;
    private readonly IResourceRepository _resourceRepository;

    public UserRepository(IAsyncUserApi userApi,
        IItemRepository itemRepository,
        IResourceRepository resourceRepository)
    {
        _userApi = userApi;
        _itemRepository = itemRepository;
        _resourceRepository = resourceRepository;
    }

    public async Task<(bool, Self)> GetSelf(ICredential c)
    {
        var (_, data) = await _userApi.TryGetSelf(c);

        var user = new Model.User(data,
            await _itemRepository.Get(data.Achievements),
            await _itemRepository.Get(data.SelectedCheckers),
            await _itemRepository.Get(data.SelectedAnimation),
            await _resourceRepository.Get(data));

        var self = new Self(user,
            await _itemRepository.Get(data.AvailableAnimations),
            await _itemRepository.Get(data.AvailableCheckersSkins),
            await _itemRepository.Get(data.AvailableLootBox),
            await _itemRepository.Get(data.Animations),
            await _itemRepository.Get(data.CheckerSkins));

        return (true, self);
    }

    public async Task<(bool, Collection)> GetCollection(ICredential credential)
    {
        var (_, self) = await GetSelf(credential);
        var selectedAnimId = self.SelectedAnimation.Id;
        var selectedSkinId = self.SelectedCheckersSkin.Id;
        var animations = self.Animations
            .Select(a => new CollectionAnimation(a, a.Id == selectedAnimId))
            .ToList();
        var skins = self.CheckersSkins
            .Select(c => new CollectionCheckersSkin(c, c.Id == selectedSkinId))
            .ToList();
        return (true, new Collection(animations, skins));
    }

    public Task<bool> SelectAnimation(ICredential credential, int id) =>
        _userApi.SelectAnimation(credential, id);

    public Task<bool> SelectCheckers(ICredential credential, int id) =>
        _userApi.SelectCheckers(credential, id);

    public Task<bool> BuyCheckersSkin(ICredential credential, int id) => 
        _userApi.BuyCheckersSkin(credential, id);
    public Task<bool> BuyAnimation(ICredential credential, int id) =>
        _userApi.BuyCheckersSkin(credential, id);

}