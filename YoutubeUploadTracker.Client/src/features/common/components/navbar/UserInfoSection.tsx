import { UserInfo as UserInfoType } from "@auth/types";
import { Avatar, Menu, Text } from "@mantine/core";
import { logout } from "@root/src/features/auth/service/auth";
import { IconLogout, IconUser } from "@tabler/icons-react";
import { useState } from "react";

type Props = {
  user: UserInfoType;
};

const iconSize = 16;

export default function UserInfo({ user }: Props) {
  const [opened, setOpened] = useState(false);

  const onLogoutClick = async () => {
    await logout();
  };

  return (
    <Menu
      trigger="click-hover"
      opened={opened}
      onChange={setOpened}
      position="bottom-end">
      <Menu.Target>
        <div className="flex flex-row items-center gap-2">
          <Avatar src={user?.avatarUrl} />
          <Text>{user.name}</Text>
        </div>
      </Menu.Target>
      <Menu.Dropdown>
        <Menu.Item leftSection={<IconUser size={iconSize} />}>
          Profile
        </Menu.Item>
        <Menu.Divider />
        <Menu.Item
          leftSection={<IconLogout size={iconSize} />}
          onClick={onLogoutClick}>
          Logout
        </Menu.Item>
      </Menu.Dropdown>
    </Menu>
  );
}
