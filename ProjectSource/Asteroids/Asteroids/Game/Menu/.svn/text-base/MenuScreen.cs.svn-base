using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Asteroids {

    /// <author>Niels Justesen, Martin Kjeldsen</author>
    /// <summary>
    /// This class contains information about a menuscreen, it's menu items, it's selectables and it's current items. Furthermore the
    /// class has features to navigate up and down in the menu, and a setup method to set a current item to a specific selectable. 
    /// </summary>
    class MenuScreen {
        
        Selectable currentItem;
        int buttonNum;
        List<Selectable> selectables = new List<Selectable>();
        List<Selectable> menuItems = new List<Selectable>();

        /// <summary>
        /// A list of menu items.
        /// </summary>
        public List<Selectable> MenuItems {
            get { return menuItems; }
            set { menuItems = value; }
        }

        /// <summary>
        /// A list of selectables.
        /// </summary>
        public List<Selectable> Selectables {
            get { return selectables; }
            set { selectables = value; }
        }

        /// <summary>
        /// A list of current items.
        /// </summary>
        public Selectable CurrentItem {
            get { return currentItem; }
            set { currentItem = value; }
        }

        public MenuScreen() {
            buttonNum = 0;
        }

        /// <summary>
        /// This method increases a button number and a current item is set to a button number if there is navigated down in the menu.  
        /// If the last button is reached then button number will be set to zero.
        /// </summary>
        public void Down() {
            buttonNum++;
            if (buttonNum + 1 > selectables.Count) {
                buttonNum = 0;
            }
            if (selectables.Count != 0) {
                currentItem = selectables[buttonNum];
            }
        }

        /// <summary>
        /// This method decreases a buttonnumber and a current item is set to a button number if there is navigated up in the menu.
        /// If the button number is smaller than zero then the button number is set to the number of buttons minus one.  
        /// </summary>
        public void Up() {
            buttonNum--;
            if (buttonNum < 0) {
                buttonNum = selectables.Count - 1;
            }
            if (selectables.Count != 0) {
                currentItem = selectables[buttonNum];
            }
        }

        /// <summary>
        /// A method to set a current item to a specific selectable.
        /// </summary>
        public void Setup() {
            buttonNum = 0; 
            if (selectables.Count > 0) {
                currentItem = selectables[buttonNum];
            }
        }
    }

}



