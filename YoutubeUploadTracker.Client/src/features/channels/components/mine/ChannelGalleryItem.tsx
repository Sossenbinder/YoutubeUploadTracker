import { Channel } from "../../types";
import { Text } from "@mantine/core";
import styles from "./ChannelGalleryItem.module.scss";
import classNames from "classnames";

type Props = {
  channel: Channel;
};

export default function ChannelGalleryItem({ channel }: Props) {
  return (
    <div
      className={classNames(
        "flex flex-row border rounded-xl p-2 gap-2 bg-[var(--mantine-color-dark-4)]",
        styles.container
      )}>
      <img className="rounded-xl" src={channel.thumbnail88} />
      <div>
        <Text>{channel.name}</Text>
      </div>
    </div>
  );
}
