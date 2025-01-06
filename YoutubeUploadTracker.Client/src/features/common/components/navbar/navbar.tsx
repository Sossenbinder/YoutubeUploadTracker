import { Burger, Switch, useMantineColorScheme } from "@mantine/core";
import { IconCloud, IconMoon, IconSun } from "@tabler/icons-react";
import { navigate } from "vike/client/router";
import Search from "./Search";
import SignIn from "./SignIn";
import UserInfoSection from "./UserInfoSection";
import useUser from "@root/src/features/auth/hooks/useUser";

type Props = {
  opened: boolean;
  toggle: () => void;
};

export default function Navbar({ opened, toggle }: Props) {
  const { colorScheme, toggleColorScheme } = useMantineColorScheme();

  const user = useUser();

  return (
    <div className="h-full p-4 flex flex-row content-center gap-4 justify-between">
      <div className="flex flex-row">
        <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
        <div
          className="flex flex-row gap-2 items-center cursor-pointer"
          onClick={() => navigate("/")}>
          <IconCloud size={24} />
          <span className="text-lg font-semibold">Upload Tracker</span>
        </div>
      </div>
      <div className="flex flex-row items-center gap-4">
        <div className="flex flex-row gap-2 items-center">
          <IconSun size={24} />
          <Switch
            checked={colorScheme === "dark"}
            onChange={toggleColorScheme}
          />
          <IconMoon size={24} />
        </div>
        <Search />
        {user ? <UserInfoSection user={user!} /> : <SignIn />}
      </div>
    </div>
  );
}
