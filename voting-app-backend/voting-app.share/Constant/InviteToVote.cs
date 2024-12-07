using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.share.Constant
{
    public static class InviteToVote
    {
        public const string InviteToVoteBody = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Email Invitation</title>
            </head>
            <body>
                <p>Người dùng <b>{{0}}</b> mời bạn tham gia bỏ phiếu cho cuộc bình chọn <b>{{1}}</b>.</p>
                <p>Truy cập vào đường link dưới đây để tham gia:</p>
                <a href='{{2}}' target='_blank'>Tham gia bình chọn</a>
            </body>
            </html>
        ";


        public const string InviteToVoteTitle = "VotingApp - Thư mời tham gia bỏ phiếu";

    }
}
