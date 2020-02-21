using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Session1GlobalLibrary;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Session1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddResources : ContentPage
    {
        List<Skill> Skill = new List<Skill>();
        List<int> selectedSkill = new List<int>();
        List<Resource_Type> list = new List<Resource_Type>();
        public AddResources()
        {
            InitializeComponent();
            SkillLoad();
            ResourceTypeLoad();
        }

        private async void SkillLoad()
        {
            var webApi = new API();
            Skill = await webApi.GetSkillsAsync();
        }
        private async void ResourceTypeLoad()
        {
            var webApi = new API();
            list = await webApi.GetResource_TypesAsync();
            foreach (var item in list)
            {
                pResourceType.Items.Add(item.resTypeName);
            }
        }

        private void cbWebTech_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var getID = Skill.Where(x => x.skillName == "Web Tech").Select(x => x.skillId).FirstOrDefault();
            if (e.Value == cbWebTech.IsChecked)
            {
                if (!selectedSkill.Contains(getID))
                {
                   
                    selectedSkill.Add(getID);
                }
            }
            else
            {
                selectedSkill.Remove(getID);
            }
        }

        private void cbNetworking_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var getID = Skill.Where(x => x.skillName == "Networking").Select(x => x.skillId).FirstOrDefault();
            if (e.Value == cbNetworking.IsChecked)
            {
                if (!selectedSkill.Contains(getID))
                {
                    selectedSkill.Add(getID);
                }
            }
            else
            {
                selectedSkill.Remove(getID);
            }
        }

        private void cbSoftwareSolutions_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var getID = Skill.Where(x => x.skillName == "Software Solutions").Select(x => x.skillId).FirstOrDefault();
            if (e.Value == cbSoftwareSolutions.IsChecked)
            {
                if (!selectedSkill.Contains(getID))
                {
                    selectedSkill.Add(getID);
                }
            }
            else
            {
                selectedSkill.Remove(getID);
            }
        }

        private void cbCyberSecurity_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var getID = Skill.Where(x => x.skillName == "Cyber Security").Select(x => x.skillId).FirstOrDefault();
            if (e.Value == cbCyberSecurity.IsChecked)
            {
                if (!selectedSkill.Contains(getID))
                {
                    selectedSkill.Add(getID);
                }
            }
            else
            {
                selectedSkill.Remove(getID);
            }
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            Resource resource = new Resource()
            {
                resName = entryResourceName.Text,
                remainingQuantity = Int32.Parse(entryQuantity.Text),
                resTypeIdFK = list.Where(x => x.resTypeName == pResourceType.SelectedItem.ToString()).Select(x => x.resTypeId).FirstOrDefault()
            };
            var webApi = new API();
            var response = Int32.Parse(await webApi.PostAsync("Resources/Create", resource));
            foreach (var item in selectedSkill)
            {
                Resource_Allocation resource_Allocation = new Resource_Allocation() { resIdFK = response, skillIdFK = item };
                await webApi.PostAsync("Resource_Allocation/Create", resource_Allocation);
            }
            await DisplayAlert("Add Resource", "Resource added and allocated successfully", "Ok");
        }
    }
}