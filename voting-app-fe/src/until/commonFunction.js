
let registerDialogHandler = null;
let registerPopupHandler = null;

export default  {
    initDialog (handler) {
        registerDialogHandler = handler;
    },

    initPopup(handler) {
        registerPopupHandler = handler;
    },

    async showDialog(title, content, buttons) {
        const result = await registerDialogHandler({title, content, buttons});
        return result;
    },


    async showPopup({component, metaData}) {
        const result = await registerPopupHandler({component, metaData});
        return result;
    }



}
