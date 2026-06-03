require("dotenv").config();

const { Client, GatewayIntentBits } = require("discord.js");

const client = new Client({
  intents: [GatewayIntentBits.Guilds, GatewayIntentBits.GuildMembers],
});

client.once("ready", () => {
  console.log(`Bot online: ${client.user.tag}`);
});

client.on("guildMemberAdd", async (member) => {
  const channel = member.guild.channels.cache.find((c) => c.name === "welcome");

  if (!channel) return;

  channel.send(
    `🚀 Chào mừng ${member} đến với gia đình NexZap. Chúc bạn có những trải nghiệm tuyệt vời cùng cộng đồng!`,
  );
});

client.login(process.env.DISCORD_TOKEN);
