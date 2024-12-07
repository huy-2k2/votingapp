
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
    },
    
    
    formatDateTimeIntl(date) {
        date = new Date(date);
        const options = {
          day: '2-digit',
          month: '2-digit',
          year: 'numeric',
          hour: '2-digit',
          minute: '2-digit',
          second: '2-digit',
          hour12: false,
        };
        return new Intl.DateTimeFormat('en-GB', options).format(date).replace(',', '');
    }



}
